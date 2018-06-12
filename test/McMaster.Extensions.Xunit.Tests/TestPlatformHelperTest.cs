// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class TestPlatformHelperTest
    {
        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.MacOS)]
        [SkipOnOperatingSystems(OperatingSystems.Windows)]
        public void IsLinux_TrueOnLinux()
        {
            Assert.True(TestPlatformHelper.IsLinux);
            Assert.False(TestPlatformHelper.IsMac);
            Assert.False(TestPlatformHelper.IsWindows);
        }

        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.Linux)]
        [SkipOnOperatingSystems(OperatingSystems.Windows)]
        public void IsMac_TrueOnMac()
        {
            Assert.False(TestPlatformHelper.IsLinux);
            Assert.True(TestPlatformHelper.IsMac);
            Assert.False(TestPlatformHelper.IsWindows);
        }

        [SkippableFact]
        [SkipOnOperatingSystems(OperatingSystems.Linux)]
        [SkipOnOperatingSystems(OperatingSystems.MacOS)]
        public void IsWindows_TrueOnWindows()
        {
            Assert.False(TestPlatformHelper.IsLinux);
            Assert.False(TestPlatformHelper.IsMac);
            Assert.True(TestPlatformHelper.IsWindows);
        }

        [SkippableFact]
        [SkipOnRuntimes(Runtimes.CLR | Runtimes.CoreCLR | Runtimes.None)]
        public void IsMono_TrueOnMono()
        {
            Assert.True(TestPlatformHelper.IsMono);
        }

        [SkippableFact]
        [SkipOnRuntimes(Runtimes.Mono)]
        public void IsMono_FalseElsewhere()
        {
            Assert.False(TestPlatformHelper.IsMono);
        }
    }
}
