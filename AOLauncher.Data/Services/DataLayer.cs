﻿using System.Data;
using AOLauncher.Data.Helpers;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;

namespace AOLauncher.Data.Services;

public class DataLayer : IDataLayer
{
    public DataLayer(string filename)
    {
        this.filename = filename;

        InstallationsTable = CreateTable(null, "Installations", "Name", "Path");
        AccountsTable = CreateTable(InstallationsTable, "Accounts", "Username", "Password");

        data.Tables.Add(InstallationsTable);
        data.Tables.Add(AccountsTable);

        data.Relations.Add("r1", InstallationsTable.Columns["Name"]!, AccountsTable.Columns["ParentKey"]!);

        data.AcceptChanges();
    }

    public async Task<List<Installation>> GetInstallationsAsync()
    {
        await LoadAsync().ConfigureAwait(false);

        return InstallationsTable
            .Rows
            .Cast<DataRow>()
            .Select(row => row.ToInstallation())
            .ToList();
    }

    public async Task SaveInstallationsAsync(List<Installation> installations)
    {
        // Clear doesn't do cascade deletes, so we can't call it but need to do that explicitly
        for (var i = InstallationsTable.Rows.Count - 1; i >= 0; i--)
            InstallationsTable.Rows.RemoveAt(i);

        foreach (var installation in installations)
        {
            InstallationsTable.Rows.Add(installation.Name, installation.Path);
            foreach (var account in installation.Accounts)
                AccountsTable.Rows.Add(installation.Name, account.Username, account.Password);
        }

        await SaveAsync().ConfigureAwait(false);
    }

    //

    private readonly DataSet data = new();

    private readonly string filename;

    private DataTable InstallationsTable { get; }
    private DataTable AccountsTable { get; }

    private static DataTable CreateTable(DataTable? parentTable, string tableName, params string[] columnNames)
    {
        var table = new DataTable(tableName);

        if (parentTable is not null)
            table.Columns.Add("ParentKey", Type.GetType("System.String")!);

        foreach (var columnName in columnNames)
            table.Columns.Add(columnName, Type.GetType("System.String")!);

        return table;
    }

    // this is async because operations that deal with the outside world should be async
    private async Task LoadAsync()
    {
        await Task.Delay(1).ConfigureAwait(false);

        try
        {
            data.Clear();
            data.ReadXml(filename);
        }
        catch
        {
            // do nothing
        }
    }

    // this is async because operations that deal with the outside world should be async
    private async Task SaveAsync()
    {
        await Task.Delay(1).ConfigureAwait(false);

        data.WriteXml(filename);
        data.AcceptChanges();
    }
}