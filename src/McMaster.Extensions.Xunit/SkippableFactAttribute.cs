// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using McMaster.Extensions.Xunit.Internal;
using Xunit;
using Xunit.Sdk;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    ///     Attribute applied to a method to indicate that it is a fact to be run by the test runner,
    ///     but only if all other attributes on the method or class which implement <see cref="ITestCondition" /> have the test
    ///     conditions satisfied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [XunitTestCaseDiscoverer("McMaster.Extensions.Xunit.Internal." + nameof(SkippableFactDiscoverer),
        "McMaster.Extensions.Xunit")]
    public class SkippableFactAttribute : FactAttribute
    {
    }
}