// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class SkipOnOSAttributeTests
    {
        [Theory]
        [InlineData("2.5", "2.5")]
        [InlineData("blue", "Blue")]
        public void Skips_WhenVersionsMatches(string currentOSVersion, string skipVersion)
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(
                OS.Windows,
                OS.Windows,
                currentOSVersion,
                skipVersion);

            // Assert
            Assert.False(osSkipAttribute.IsMet);
        }

        [Fact]
        public void DoesNotSkip_WhenOnlyVersionsMatch()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(
                OS.Linux,
                OS.Windows,
                "2.5",
                "2.5");

            // Assert
            Assert.True(osSkipAttribute.IsMet);
        }

        [Fact]
        public void DoesNotSkip_WhenOperatingSystemDoesNotMatch()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(
                OS.Linux,
                OS.Windows,
                "2.5");

            // Assert
            Assert.True(osSkipAttribute.IsMet);
        }

        [Fact]
        public void DoesNotSkip_WhenVersionsDoNotMatch()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(
                OS.Windows,
                OS.Windows,
                "2.5",
                "10.0");

            // Assert
            Assert.True(osSkipAttribute.IsMet);
        }

        [Fact]
        public void Skips_BothMacOSXAndLinux()
        {
            // Act
            var osSkipAttributeLinux = new SkipOnOSAttribute(OS.Linux | OS.MacOS, OS.Linux, string.Empty);
            var osSkipAttributeMacOSX = new SkipOnOSAttribute(OS.Linux | OS.MacOS, OS.MacOS, string.Empty);

            // Assert
            Assert.False(osSkipAttributeLinux.IsMet);
            Assert.False(osSkipAttributeMacOSX.IsMet);
        }

        [Fact]
        public void Skips_BothMacOSXAndWindows()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(OS.Windows | OS.MacOS, OS.Windows, string.Empty);
            var osSkipAttributeMacOSX = new SkipOnOSAttribute(OS.Windows | OS.MacOS, OS.MacOS, string.Empty);

            // Assert
            Assert.False(osSkipAttribute.IsMet);
            Assert.False(osSkipAttributeMacOSX.IsMet);
        }

        [Fact]
        public void Skips_BothWindowsAndLinux()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(OS.Linux | OS.Windows, OS.Windows, string.Empty);
            var osSkipAttributeLinux = new SkipOnOSAttribute(OS.Linux | OS.Windows, OS.Linux, string.Empty);

            // Assert
            Assert.False(osSkipAttribute.IsMet);
            Assert.False(osSkipAttributeLinux.IsMet);
        }

        [Fact]
        public void Skips_WhenOnlyOperatingSystemIsSupplied()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(
                OS.Windows,
                OS.Windows,
                "2.5");

            // Assert
            Assert.False(osSkipAttribute.IsMet);
        }

        [Fact]
        public void Skips_WhenVersionsMatchesOutOfMultiple()
        {
            // Act
            var osSkipAttribute = new SkipOnOSAttribute(
                OS.Windows,
                OS.Windows,
                "2.5",
                "10.0", "3.4", "2.5");

            // Assert
            Assert.False(osSkipAttribute.IsMet);
        }
    }
}