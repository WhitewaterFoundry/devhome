// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using WSLExtension.Exceptions;
using WSLExtension.Models;
using WSLExtension.Services;

namespace WSLExtension.Helpers.Distros;

public class DistroState
{
    public static void Run(string registration, bool isRoot, IProcessCaller processCaller)
    {
        var rootOption = isRoot ? "-u root" : string.Empty;

        processCaller.CallDetachedProcess(
            "wsl",
            $"-d {registration} {rootOption} -- cd ~;$(getent passwd $LOGNAME | cut -d: -f7) --login");
    }

    public static void Terminate(string registration, IProcessCaller processCaller)
    {
        processCaller.CallProcess(
            "wsl",
            $"--terminate {registration}",
            exitCode: out var exitCode);

        if (exitCode != 0)
        {
            throw new WslManagerException($"Failed to terminate the distro {registration}");
        }
    }
}
