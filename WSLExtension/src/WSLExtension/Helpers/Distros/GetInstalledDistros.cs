// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using WSLExtension.Models;
using WSLExtension.Services;

namespace WSLExtension.Helpers.Distros;

public class GetInstalledDistros
{
    public static List<IDistro> Execute(IProcessCaller processCaller)
    {
        var distroListDetail = processCaller.CallProcess("wsl", "--list --verbose", out var exitCode);
        if (AreDistributionsInstalled(exitCode, distroListDetail))
        {
            return new List<IDistro>();
        }

        return WslCommandUtils.ParseDistroListDetail(distroListDetail);
    }

    private static bool AreDistributionsInstalled(int exitCode, string commandOutput)
    {
        return !commandOutput.Contains("NAME")
               || commandOutput.Contains("Wsl/WSL_E_DEFAULT_DISTRO_NOT_FOUND")
               || exitCode != 0;
    }
}
