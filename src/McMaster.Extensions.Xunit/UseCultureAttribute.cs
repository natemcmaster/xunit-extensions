// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    /// Replaces the current culture and UI culture for the test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UseCultureAttribute : BeforeAfterTestAttribute
    {
        private CultureInfo _originalCulture;
        private CultureInfo _originalUICulture;

        /// <summary>
        /// Replaces the current culture with invariant culture.
        /// </summary>
        public UseCultureAttribute()
            : this(CultureInfo.InvariantCulture)
        {
        }

        /// <summary>
        /// Replaces the thread culture based on specified values.
        /// </summary>
        public UseCultureAttribute(string culture)
            : this(culture, culture)
        {
        }

        /// <summary>
        /// Replaces the current culture and UI culture based on specified values.
        /// </summary>
        public UseCultureAttribute(string testCulture, string testUICulture)
        {
            Culture = new CultureInfo(testCulture);
            UICulture = new CultureInfo(testUICulture);
        }

        /// <summary>
        /// Replaces the current culture and UI culture based on specified values.
        /// </summary>
        public UseCultureAttribute(CultureInfo culture)
        {
            Culture = culture;
            UICulture = culture;
        }

        /// <summary>
        /// The <see cref="CultureInfo.CurrentCulture"/> for the test. Defaults to en-US.
        /// </summary>
        public CultureInfo Culture { get; }

        /// <summary>
        /// The <see cref="CultureInfo.CurrentUICulture"/> for the test. Defaults to en-US.
        /// </summary>
        public CultureInfo UICulture { get; }

        /// <inheritdoc />
        public override void Before(MethodInfo methodUnderTest)
        {
            _originalCulture = Thread.CurrentThread.CurrentCulture;
            _originalUICulture = Thread.CurrentThread.CurrentUICulture;

            Thread.CurrentThread.CurrentCulture = Culture;
            Thread.CurrentThread.CurrentUICulture = UICulture;

            CultureInfo.CurrentCulture.ClearCachedData();
            CultureInfo.CurrentUICulture.ClearCachedData();
        }

        /// <inheritdoc />
        public override void After(MethodInfo methodUnderTest)
        {
            Thread.CurrentThread.CurrentCulture = _originalCulture;
            Thread.CurrentThread.CurrentUICulture = _originalUICulture;

            CultureInfo.CurrentCulture.ClearCachedData();
            CultureInfo.CurrentUICulture.ClearCachedData();
        }
    }
}

