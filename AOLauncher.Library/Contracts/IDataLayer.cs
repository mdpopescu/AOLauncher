﻿using AOLauncher.Library.Models;

namespace AOLauncher.Library.Contracts;

public interface IDataLayer
{
    Task<List<Installation>> GetInstallationsAsync();
    Task SaveInstallationsAsync(List<Installation> installations);
}