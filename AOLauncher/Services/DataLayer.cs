using System.Data;
using AOLauncher.Models;

namespace AOLauncher.Services;

public class DataLayer
{
    public DataTable InstallationsTable { get; }

    public DataLayer()
    {
        InstallationsTable = CreateTable(null, "Installations", "Name", "Path");
        AccountsTable = CreateTable(InstallationsTable, "Accounts", "Username", "Password");

        data.Tables.Add(InstallationsTable);
        data.Tables.Add(AccountsTable);

        data.Relations.Add("r1", InstallationsTable.Columns["Id"]!, AccountsTable.Columns["ParentId"]!);

        data.AcceptChanges();
    }

    public void Load(string filename)
    {
        try
        {
            data.ReadXml(filename);
        }
        catch
        {
            // do nothing
        }
    }

    public void Save(string filename)
    {
        data.WriteXml(filename);
    }

    // ReSharper disable once ReturnTypeCanBeEnumerable.Global
    public string[] GetInstallationNames() => InstallationsTable
        .Rows
        .Cast<DataRow>()
        .Select(row => row["Name"] + "")
        .ToArray();

    // ReSharper disable once ReturnTypeCanBeEnumerable.Global
    public Account[] GetAccounts(string installationName) => InstallationsTable
        .Rows
        .Cast<DataRow>()
        .Where(row => string.Equals(row["Name"] + "", installationName, StringComparison.OrdinalIgnoreCase))
        .Single()
        .GetChildRows("r1")
        .Select(row => new Account(row["Username"] + "", row["Password"] + ""))
        .ToArray();

    public DataTable GetAccountsTable(string installationName)
    {
        var table = new DataTable();

        table.Columns.Add("Username", Type.GetType("System.String")!);
        table.Columns.Add("Password", Type.GetType("System.String")!);

        table.Rows.Add(GetAccounts(installationName).Cast<object>().ToArray());
        table.AcceptChanges();

        return table;
    }

    public void UpdateAccounts(string installationName, DataTable accountsTable)
    {
        var installationId = InstallationsTable
            .Rows
            .Cast<DataRow>()
            .Where(row => string.Equals(row["Name"] + "", installationName, StringComparison.OrdinalIgnoreCase))
            .Select(row => (int)row["Id"])
            .Single();

        var toDelete = AccountsTable.Rows.Cast<DataRow>().Where(row => (int)row["ParentId"] == installationId);
        foreach (var row in toDelete)
            row.Delete();

        foreach (var row in accountsTable.Rows.Cast<DataRow>())
            AccountsTable.Rows.Add(null, installationId, row["Username"] + "", row["Password"] + "");

        AccountsTable.AcceptChanges();
    }

    //

    private readonly DataSet data = new();

    private DataTable AccountsTable { get; }

    private static DataTable CreateTable(DataTable? parentTable, string tableName, params string[] columnNames)
    {
        var table = new DataTable(tableName);

        var idColumn = new DataColumn("Id", Type.GetType("System.Int32")!);
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        table.Columns.Add(idColumn);

        if (parentTable is not null)
            table.Columns.Add("ParentId", Type.GetType("System.Int32")!);

        foreach (var columnName in columnNames)
            table.Columns.Add(columnName, Type.GetType("System.String")!);

        return table;
    }
}