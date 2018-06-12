// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    /// Defines a test condition which must be met before a test can be run.
    /// </summary>
    public interface ITestCondition
    {
        /// <summary>
        /// Is the test condition satisifed?
        /// </summary>
        bool IsMet { get; }

        /// <summary>
        /// The reason the test condition was not met.
        /// </summary>
        string SkipReason { get; }
    }
}