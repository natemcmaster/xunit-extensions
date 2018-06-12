// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace McMaster.Extensions.Xunit
{
    /// <summary>
    ///     Skip this test on certain runtiem frameworks.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SkipOnRuntimesAttribute : Attribute, ITestCondition
    {
        private readonly Runtimes _skippedRuntimes;

        /// <summary>
        ///     Initialize an instance
        /// </summary>
        /// <param name="skippedRuntimes"></param>
        public SkipOnRuntimesAttribute(Runtimes skippedRuntimes)
        {
            _skippedRuntimes = skippedRuntimes;
        }

        /// <inheritdoc />
        public bool IsMet => CanRunOnCurrentRuntime(_skippedRuntimes);

        /// <inheritdoc />
        public string SkipReason { get; set; } = "Test cannot run on this runtime framework.";

        private static bool CanRunOnCurrentRuntime(Runtimes skippedRuntimes)
        {
            if (skippedRuntimes == Runtimes.None) return true;

            if (TestPlatformHelper.IsNETCore)
            {
                if (skippedRuntimes.HasFlag(Runtimes.NETCore)) return false;
            }
            else
            {
                if (skippedRuntimes.HasFlag(Runtimes.Mono) &&
                    TestPlatformHelper.IsMono)
                    return false;

                if (skippedRuntimes.HasFlag(Runtimes.NETFramework)) return false;
            }

            return true;
        }
    }
}