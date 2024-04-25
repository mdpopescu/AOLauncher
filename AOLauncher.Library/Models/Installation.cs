using System.Diagnostics.CodeAnalysis;

namespace AOLauncher.Library.Models;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Installation
{
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
    public List<Account> Accounts { get; set; } = [];
}