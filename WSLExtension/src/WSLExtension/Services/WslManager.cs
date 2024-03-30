// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using WSLExtension.Common;
using WSLExtension.DistroDefinitions;
using WSLExtension.Helpers.Distros;
using WSLExtension.Models;

namespace WSLExtension.Services;

public class WslManager : IWslManager
{
    private readonly IProcessCaller _processCaller;
    private readonly IRegistryAccess _registryAccess;
    private readonly IStringResource _stringResource;

    private readonly List<IDistro> _distroDefinitions;

    public WslManager(IProcessCaller processCaller, IRegistryAccess registryAccess, IStringResource stringResource)
    {
        _processCaller = processCaller;
        _registryAccess = registryAccess;
        _stringResource = stringResource;
        _distroDefinitions = [];

        ReadDistroDefinitions();
    }

    private void ReadDistroDefinitions()
    {
        Task.Run(async () =>
        {
            var definitionsRead = await DistroDefinitionsManager.ReadDistroDefinitions();
            _distroDefinitions.AddRange(definitionsRead);
        });
    }

    public IEnumerable<WslRegisteredDistro> GetAllRegisteredDistros()
    {
        var distros = DistroDefinitionsManager.Merge(
            _distroDefinitions,
            GetInstalledDistros.Execute(_processCaller));

        return distros.Select(d => new WslRegisteredDistro(_stringResource, this)
        {
            DisplayName = d.Name ?? d.Registration,
            SupplementalDisplayName = d.Name != null && d.Name != d.Registration ? d.Registration : string.Empty,
            Running = d.Running,
            Id = d.Registration,
            IsDefault = d.DefaultDistro,
            IsWsl2 = d.Version2,
            Logo = d.Logo,
        });
    }

    public void Run(string registration)
    {
        DistroState.Run(registration, isRoot: false, _processCaller);
    }

    public void Terminate(string registration)
    {
        DistroState.Terminate(registration, _processCaller);
    }

    public void Unregister(string registration)
    {
        Management.Unregister(registration, _processCaller);
    }
}
