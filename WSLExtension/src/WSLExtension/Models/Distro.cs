// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WSLExtension.Models;

public class Distro : IDistro, IComparable<IDistro>, ICloneable
{
    public Distro()
    {
    }

    public Distro(string registration)
    {
        Registration = registration;
    }

    public static IComparer<IDistro> RegistrationComparer { get; } = new RegistrationRelationalComparer();

    public static IComparer<IDistro> NameRegistrationComparer { get; } = new NameRegistrationRelationalComparer();

    public static IComparer<IDistro> DistroKindRegistrationNameComparer { get; } =
        new DistroKindRegistrationNameRelationalComparer();

    public object Clone()
    {
        return new Distro(Registration)
        {
            DefaultDistro = DefaultDistro,
            Logo = Logo,
            Name = Name,
            Running = Running,
            Version2 = Version2,
        };
    }

    public int CompareTo(IDistro? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        return string.Compare(Registration, other.Registration, StringComparison.Ordinal);
    }

    public string? Logo { get; set; }

    public string? Name { get; set; }

    public string Registration { get; set; } = null!;

    public bool? Running { get; set; }

    public bool? DefaultDistro { get; set; }

    public bool? Version2 { get; set; }

    public bool? HasArm64Version { get; set; }

    protected bool Equals(IDistro? other)
    {
        return other != null && Registration == other.Registration;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is IDistro distro)
        {
            return Equals(distro);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Registration.GetHashCode();
    }

    private sealed class RegistrationRelationalComparer : IComparer<IDistro>
    {
        public int Compare(IDistro? x, IDistro? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(null, y))
            {
                return 1;
            }

            if (ReferenceEquals(null, x))
            {
                return -1;
            }

            return string.Compare(x.Registration, y.Registration, StringComparison.Ordinal);
        }
    }

    private sealed class NameRegistrationRelationalComparer : IComparer<IDistro>
    {
        public int Compare(IDistro? x, IDistro? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(null, y))
            {
                return 1;
            }

            if (ReferenceEquals(null, x))
            {
                return -1;
            }

            var nameComparison = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return string.Compare(x.Registration, y.Registration, StringComparison.Ordinal);
        }
    }

    private sealed class DistroKindRegistrationNameRelationalComparer : IComparer<IDistro>
    {
        public int Compare(IDistro? x, IDistro? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(null, y))
            {
                return 1;
            }

            if (ReferenceEquals(null, x))
            {
                return -1;
            }

            var registrationComparison = string.Compare(x.Registration, y.Registration, StringComparison.Ordinal);
            if (registrationComparison != 0)
            {
                return registrationComparison;
            }

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }

    public static bool operator ==(Distro? left, Distro? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Distro left, Distro right)
    {
        return !(left == right);
    }

    public static bool operator <(Distro left, Distro right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Distro left, Distro right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Distro left, Distro right)
    {
        var result = left.CompareTo(right);

        return result is < 0 or 0;
    }

    public static bool operator >=(Distro left, Distro right)
    {
        var result = left.CompareTo(right);

        return result is > 0 or 0;
    }
}
