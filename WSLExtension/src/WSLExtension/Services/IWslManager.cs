// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSLExtension.Models;

namespace WSLExtension.Services;

public interface IWslManager
{
    /// <summary> Gets a list of Hyper-V virtual machines.</summary>
    /// <returns> A list of virtual machines.</returns>
    public IEnumerable<WslRegisteredDistro> GetAllRegisteredDistros();

    void Run(string registration);

    void Terminate(string registration);

    void Unregister(string registration);
}
