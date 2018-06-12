// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class UseCultureAttributeTests
    {
        [Fact]
        public void BeforeAndAfterTest_ReplacesCulture()
        {
            // Arrange
            var originalCulture = CultureInfo.CurrentCulture;
            var originalUICulture = CultureInfo.CurrentUICulture;
            var culture = "de-DE";
            var uiCulture = "fr-CA";
            var replaceCulture = new UseCultureAttribute(culture, uiCulture);

            // Act
            replaceCulture.Before(null);

            // Assert
            Assert.Equal(new CultureInfo(culture), CultureInfo.CurrentCulture);
            Assert.Equal(new CultureInfo(uiCulture), CultureInfo.CurrentUICulture);

            // Act
            replaceCulture.After(null);

            // Assert
            Assert.Equal(originalCulture, CultureInfo.CurrentCulture);
            Assert.Equal(originalUICulture, CultureInfo.CurrentUICulture);
        }

        [Fact]
        public void UsesSuppliedCultureAndUICulture()
        {
            // Arrange
            var culture = "de-DE";
            var uiCulture = "fr-CA";

            // Act
            var replaceCulture = new UseCultureAttribute(culture, uiCulture);

            // Assert
            Assert.Equal(new CultureInfo(culture), replaceCulture.Culture);
            Assert.Equal(new CultureInfo(uiCulture), replaceCulture.UICulture);
        }
    }
}