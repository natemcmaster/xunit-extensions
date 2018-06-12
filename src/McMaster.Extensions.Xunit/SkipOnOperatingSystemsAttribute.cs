// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using McMaster.Extensions.Xunit;

namespace Xunit
{
    /// <summary>
    /// Skip tests on specified operating systems.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
    public class SkipOnOperatingSystemsAttribute : Attribute, ITestCondition
    {
        private readonly OperatingSystems _excludedOperatingSystems;
        private readonly IEnumerable<string> _excludedVersions;
        private readonly OperatingSystems _osPlatform;
        private readonly string _osVersion;

        /// <summary>
        /// Initialize an instance of <see cref="SkipOnOperatingSystemsAttribute"/>.
        /// </summary>
        /// <param name="OperatingSystems"></param>
        /// <param name="versions"></param>
        public SkipOnOperatingSystemsAttribute(OperatingSystems OperatingSystems, params string[] versions) :
            this(
                OperatingSystems,
                GetCurrentOS(),
                GetCurrentOSVersion(),
                versions)
        {
        }

        // to enable unit testing
        internal SkipOnOperatingSystemsAttribute(
            OperatingSystems OperatingSystems, OperatingSystems osPlatform, string osVersion, params string[] versions)
        {
            _excludedOperatingSystems = OperatingSystems;
            _excludedVersions = versions ?? Enumerable.Empty<string>();
            _osPlatform = osPlatform;
            _osVersion = osVersion;
        }

        /// <inheritdoc />
        public bool IsMet
        {
            get
            {
                var currentOSInfo = new OSInfo()
                {
                    OperatingSystems = _osPlatform,
                    Version = _osVersion,
                };

                var skip = (_excludedOperatingSystems & currentOSInfo.OperatingSystems) == currentOSInfo.OperatingSystems;
                if (_excludedVersions.Any())
                {
                    skip = skip
                        && _excludedVersions.Any(ex => _osVersion.StartsWith(ex, StringComparison.OrdinalIgnoreCase));
                }

                // Since a test would be excuted only if 'IsMet' is true, return false if we want to skip
                return !skip;
            }
        }

        /// <inheritdoc />
        public string SkipReason { get; set; } = "Test cannot run on this operating system.";

        static private OperatingSystems GetCurrentOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OperatingSystems.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OperatingSystems.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OperatingSystems.MacOS;
            }
            throw new PlatformNotSupportedException();
        }

        static private string GetCurrentOSVersion()
            // currently not used on other OS's
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Environment.OSVersion.Version.ToString()
                : string.Empty;

        private class OSInfo
        {
            public OperatingSystems OperatingSystems { get; set; }

            public string Version { get; set; }
        }
    }
}
