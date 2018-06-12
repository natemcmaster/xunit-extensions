// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    ///     Helper class for identifying information about the current test platform.
    /// </summary>
    public static class TestPlatformHelper
    {
        /// <summary>
        ///     Currently running on the Mono
        /// </summary>
        public static bool IsMono =>
            Type.GetType("Mono.Runtime") != null;

        /// <summary>
        ///     Currently running on .NET Core
        /// </summary>
        public static bool IsNETCore =>
            RuntimeInformation.FrameworkDescription.Contains(".NET Core");

        /// <summary>
        ///     Currently running on .NET Framework
        /// </summary>
        public static bool IsNETFramework =>
            IsWindows && !IsNETCore;
        
        /// <summary>
        ///     Currently running on Windows
        /// </summary>
        public static bool IsWindows =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        ///     Currently running on Linux
        /// </summary>
        public static bool IsLinux =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        ///     Currently running on Windows
        /// </summary>
        public static bool IsMacOS =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}