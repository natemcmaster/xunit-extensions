// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    ///     Operating systems
    /// </summary>
    [Flags]
    public enum OS
    {
        /// <summary>
        ///     Linux
        /// </summary>
        Linux = 1,

        /// <summary>
        ///     macOS
        /// </summary>
        MacOS = 2,

        /// <summary>
        ///     Windows
        /// </summary>
        Windows = 4
    }
}