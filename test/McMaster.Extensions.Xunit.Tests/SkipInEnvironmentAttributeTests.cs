// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using McMaster.Extensions.Xunit.Internal;
using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class SkipInEnvironmentAttributeTests
    {
        private readonly string _skipReason = "Test skipped on environment variable with name '{0}' and value '{1}'" +
                                              $" for the '{nameof(SkipInEnvironmentAttribute.SkipOnMatch)}' value of '{{2}}'.";

        [Theory]
        [InlineData("false")]
        [InlineData("")]
        [InlineData(null)]
        public void IsMet_DoesNotMatch(string environmentVariableValue)
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable(environmentVariableValue),
                "Run",
                "true");

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.False(isMet);
        }

        [Theory]
        [InlineData("True")]
        [InlineData("TRUE")]
        [InlineData("true")]
        public void IsMet_DoesCaseInsensitiveMatch_OnValue(string environmentVariableValue)
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable(environmentVariableValue),
                "Run",
                "true");

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.True(isMet);
            Assert.Equal(
                string.Format(_skipReason, "Run", environmentVariableValue, attribute.SkipOnMatch),
                attribute.SkipReason);
        }

        [Theory]
        [InlineData("false")]
        [InlineData("")]
        [InlineData(null)]
        public void IsMet_MatchesOnMultipleSkipValues(string environmentVariableValue)
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable(environmentVariableValue),
                "Run",
                "false", "", null);

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.True(isMet);
        }

        [Theory]
        [InlineData("CentOS")]
        [InlineData(null)]
        [InlineData("")]
        public void IsMet_Matches_WhenSkipOnMatchIsFalse(string environmentVariableValue)
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable(environmentVariableValue),
                "LinuxFlavor",
                "Ubuntu14.04")
            {
                // Example: Run this test on all OSes except on "Ubuntu14.04"
                SkipOnMatch = false
            };

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.True(isMet);
        }

        private struct TestEnvironmentVariable : IEnvironmentVariable
        {
            public TestEnvironmentVariable(string value)
            {
                Value = value;
            }

            public string Value { get; }

            public string Get(string name)
            {
                return Value;
            }
        }

        [Fact]
        public void IsMet_DoesNotMatch_OnMultipleSkipValues()
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable("100"),
                "Build",
                "125", "126");

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.False(isMet);
        }

        [Fact]
        public void IsMet_DoesNotMatch_WhenSkipOnMatchIsFalse()
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable("Ubuntu14.04"),
                "LinuxFlavor",
                "Ubuntu14.04")
            {
                // Example: Run this test on all OSes except on "Ubuntu14.04"
                SkipOnMatch = false
            };

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.False(isMet);
        }

        [Fact]
        public void IsMet_DoesSuccessfulMatch_OnNull()
        {
            // Arrange
            var attribute = new SkipInEnvironmentAttribute(
                new TestEnvironmentVariable(null),
                "Run",
                "true", null); // skip the test when the variable 'Run' is explicitly set to 'true' or is null (default)

            // Act
            var isMet = attribute.IsMet;

            // Assert
            Assert.True(isMet);
            Assert.Equal(
                string.Format(_skipReason, "Run", "(null)", attribute.SkipOnMatch),
                attribute.SkipReason);
        }
    }
}