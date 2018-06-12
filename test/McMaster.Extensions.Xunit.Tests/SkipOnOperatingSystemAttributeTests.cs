// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class SkipOnOperatingSystemsAttributeTests
    {
        [SkippableFact]
        [SkipOnOS(OS.Linux)]
        public void TestSkipLinux()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                "Test should not be running on Linux");
        }

        [SkippableFact]
        [SkipOnOS(OS.MacOS)]
        public void TestSkipMacOSX()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
                "Test should not be running on MacOSX.");
        }

        [SkippableFact]
        [SkipOnOS(OS.Windows, WindowsVersions.Win7, WindowsVersions.Win2008R2)]
        public void RunTest_DoesNotRunOnWin7OrWin2008R2()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                Environment.OSVersion.Version.ToString().StartsWith("6.1"),
                "Test should not be running on Win7 or Win2008R2.");
        }

        [SkippableFact]
        [SkipOnOS(OS.Windows)]
        public void TestSkipWindows()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                "Test should not be running on Windows.");
        }

        [SkippableFact]
        [SkipOnOS(OS.Linux | OS.MacOS)]
        public void TestSkipLinuxAndMacOSX()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                "Test should not be running on Linux.");
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
                "Test should not be running on MacOSX.");
        }

        [SkippableTheory]
        [SkipOnOS(OS.Linux)]
        [InlineData(1)]
        public void TestTheorySkipLinux(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                "Test should not be running on Linux");
        }

        [SkippableTheory]
        [SkipOnOS(OS.MacOS)]
        [InlineData(1)]
        public void TestTheorySkipMacOS(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
                "Test should not be running on MacOSX.");
        }

        [SkippableTheory]
        [SkipOnOS(OS.Windows)]
        [InlineData(1)]
        public void TestTheorySkipWindows(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                "Test should not be running on Windows.");
        }

        [SkippableTheory]
        [SkipOnOS(OS.Linux | OS.MacOS)]
        [InlineData(1)]
        public void TestTheorySkipLinuxAndMacOSX(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                "Test should not be running on Linux.");
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
                "Test should not be running on MacOSX.");
        }
    }

    [SkipOnOS(OS.Windows)]
    public class OSSkipConditionClassTest
    {
        [SkippableFact]
        public void TestSkipClassWindows()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                "Test should not be running on Windows.");
        }
    }
}