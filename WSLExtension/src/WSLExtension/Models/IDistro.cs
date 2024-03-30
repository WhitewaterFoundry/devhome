// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace WSLExtension.Models;

public interface IDistro
{
    string? Logo { get; set; }

    string? Name { get; set; }

    string Registration { get; }

    bool? Running { get; set; }

    bool? DefaultDistro { get; set; }

    bool? Version2 { get; set; }

    bool? HasArm64Version { get; set; }
}
