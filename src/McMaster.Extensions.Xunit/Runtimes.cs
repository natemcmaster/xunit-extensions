// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    /// Represents various .NET Framework runtimes
    /// </summary>
    [Flags]
    public enum Runtimes
    {
        /// <summary>
        /// Represents no runtime in particular.
        /// </summary>
        None = 0,
        /// <summary>
        /// Mono
        /// </summary>
        Mono = 1 << 0,
        
        /// <summary>
        /// .NET Framework
        /// </summary>
        NETFramework = 1 << 1,
        
        /// <summary>
        /// .NET Core
        /// </summary>
        NETCore = 1 << 2,
    }
}