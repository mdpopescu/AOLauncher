using System.Diagnostics.CodeAnalysis;

namespace AOLauncher.Library.Models;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Account
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}