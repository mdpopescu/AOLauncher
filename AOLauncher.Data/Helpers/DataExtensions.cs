using System.Data;
using AOLauncher.Library.Models;

namespace AOLauncher.Data.Helpers;

internal static class DataExtensions
{
    public static Installation ToInstallation(this DataRow row) => new()
    {
        Name = row["Name"] + "",
        Path = row["Path"] + "",
        Accounts = row.GetChildRows("r1").Select(ToAccount).ToList(),
    };

    //

    private static Account ToAccount(this DataRow row) => new()
    {
        Username = row["Username"] + "",
        Password = row["Password"] + "",
    };
}