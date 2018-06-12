// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    ///     Skip tests on specified operating systems.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
    public class SkipOnOSAttribute : Attribute, ITestCondition
    {
        private readonly OS _excludedOs;
        private readonly IEnumerable<string> _excludedVersions;
        private readonly OS _osPlatform;
        private readonly string _osVersion;

        /// <summary>
        ///     Initialize an instance of <see cref="SkipOnOSAttribute" />.
        /// </summary>
        /// <param name="os"></param>
        /// <param name="versions"></param>
        public SkipOnOSAttribute(OS os, params string[] versions)
            : this(
                os,
                GetCurrentOS(),
                GetCurrentOSVersion(),
                versions)
        {
        }

        // to enable unit testing
        internal SkipOnOSAttribute(
            OS os, OS osPlatform, string osVersion, params string[] versions)
        {
            _excludedOs = os;
            _excludedVersions = versions ?? Enumerable.Empty<string>();
            _osPlatform = osPlatform;
            _osVersion = osVersion;
        }

        /// <inheritdoc />
        public bool IsMet
        {
            get
            {
                var currentOSInfo = new OSInfo
                {
                    Os = _osPlatform,
                    Version = _osVersion
                };

                var skip = (_excludedOs & currentOSInfo.Os) == currentOSInfo.Os;
                if (_excludedVersions.Any())
                    skip = skip
                           && _excludedVersions.Any(ex =>
                               _osVersion.StartsWith(ex, StringComparison.OrdinalIgnoreCase));

                // Since a test would be excuted only if 'IsMet' is true, return false if we want to skip
                return !skip;
            }
        }

        /// <inheritdoc />
        public string SkipReason { get; set; } = "Test cannot run on this operating system.";

        private static OS GetCurrentOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OS.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OS.Linux;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return OS.MacOS;
            throw new PlatformNotSupportedException();
        }

        private static string GetCurrentOSVersion()
            // currently not used on other OS's
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Environment.OSVersion.Version.ToString()
                : string.Empty;
        }

        private class OSInfo
        {
            public OS Os { get; set; }

            public string Version { get; set; }
        }
    }
}