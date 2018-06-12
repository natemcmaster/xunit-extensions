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
        [SkipOnOperatingSystems(OperatingSystems.Linux)]
        public void TestSkipLinux()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                "Test should not be running on Linux");
        }

        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.MacOS)]
        public void TestSkipMacOSX()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
                "Test should not be running on MacOSX.");
        }

        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.Windows, WindowsVersions.Win7, WindowsVersions.Win2008R2)]
        public void RunTest_DoesNotRunOnWin7OrWin2008R2()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                Environment.OSVersion.Version.ToString().StartsWith("6.1"),
                "Test should not be running on Win7 or Win2008R2.");
        }

        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.Windows)]
        public void TestSkipWindows()
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                "Test should not be running on Windows.");
        }

        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.Linux | OperatingSystems.MacOS)]
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
        [SkipOnOperatingSystems(OperatingSystems.Linux)]
        [InlineData(1)]
        public void TestTheorySkipLinux(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                "Test should not be running on Linux");
        }

        [SkippableTheory]
        [SkipOnOperatingSystems(OperatingSystems.MacOS)]
        [InlineData(1)]
        public void TestTheorySkipMacOS(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
                "Test should not be running on MacOSX.");
        }

        [SkippableTheory]
        [SkipOnOperatingSystems(OperatingSystems.Windows)]
        [InlineData(1)]
        public void TestTheorySkipWindows(int arg)
        {
            Assert.False(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                "Test should not be running on Windows.");
        }

        [SkippableTheory]
        [SkipOnOperatingSystems(OperatingSystems.Linux | OperatingSystems.MacOS)]
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

    [SkipOnOperatingSystems(OperatingSystems.Windows)]
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
