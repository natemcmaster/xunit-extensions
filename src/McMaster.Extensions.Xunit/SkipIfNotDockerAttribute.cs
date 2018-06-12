// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    ///     Skip tests if this is not running inside Docker.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SkipIfNotDockerAttribute : Attribute, ITestCondition
    {
        /// <inheritdoc />
        public string SkipReason { get; } = "This test can only run in a Docker container.";

        /// <inheritdoc />
        public bool IsMet
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return false;

                const string procFile = "/proc/1/cgroup";
                if (!File.Exists(procFile)) return false;

                var lines = File.ReadAllLines(procFile);
                // typically the last line in the file is "1:name=openrc:/docker"
                return lines.Reverse().Any(l => l.EndsWith("name=openrc:/docker", StringComparison.Ordinal));
            }
        }
    }
}