using System.Data;
using System.Drawing;
using AOLauncher.Data.Helpers;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;

namespace AOLauncher.Data.Services;

/// <remarks>
///     Per https://stackoverflow.com/a/913286/31793 we don't need to Dispose DataSets or DataTables.
/// </remarks>
public class DataLayer : IDataLayer
{
    public DataLayer(string aoSettingsFile, string uiSettingsFile)
    {
        this.aoSettingsFile = aoSettingsFile;
        this.uiSettingsFile = uiSettingsFile;

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
        // Clear doesn't do cascade deletes
        AccountsTable.Clear();
        InstallationsTable.Clear();

        foreach (var installation in installations)
        {
            InstallationsTable.Rows.Add(installation.Name, installation.Path);
            foreach (var account in installation.Accounts)
                AccountsTable.Rows.Add(installation.Name, account.Username, account.Password);
        }

        await SaveAsync().ConfigureAwait(false);
    }

    public async Task<AppSettings> LoadSettingsAsync()
    {
        try
        {
            var lines = await File.ReadAllLinesAsync(uiSettingsFile).ConfigureAwait(false);
            var location = new Point(int.Parse(lines[0]), int.Parse(lines[1]));
            var size = new Size(int.Parse(lines[2]), int.Parse(lines[3]));
            return new AppSettings(location, size, lines[4]);
        }
        catch
        {
            return new AppSettings(new Point(100, 100), new Size(478, 456), "");
        }
    }

    public async Task SaveSettingsAsync(AppSettings settings)
    {
        var lines = new[]
        {
            settings.Location.X.ToString(),
            settings.Location.Y.ToString(),
            settings.Size.Width.ToString(),
            settings.Size.Height.ToString(),
            settings.Installation,
        };
        await File.WriteAllLinesAsync(uiSettingsFile, lines).ConfigureAwait(false);
    }

    //

    private static readonly Type STRING_TYPE = Type.GetType("System.String")!;

    private readonly DataSet data = new();

    private readonly string aoSettingsFile;
    private readonly string uiSettingsFile;

    private DataTable InstallationsTable { get; }
    private DataTable AccountsTable { get; }

    private static DataTable CreateTable(DataTable? parentTable, string tableName, params string[] columnNames)
    {
        var table = new DataTable(tableName);

        if (parentTable is not null)
            table.Columns.Add("ParentKey", STRING_TYPE);

        foreach (var columnName in columnNames)
            table.Columns.Add(columnName, STRING_TYPE);

        return table;
    }

    // this is async because operations that deal with the outside world should be async
    private async Task LoadAsync()
    {
        await Task.Delay(1).ConfigureAwait(false);

        try
        {
            data.Clear();
            data.ReadXml(aoSettingsFile);
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

        data.WriteXml(aoSettingsFile);
        data.AcceptChanges();
    }
}