// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class SkippableFactTests : IClassFixture<SkippableFactTests.SkippableFactAsserter>
    {
        public SkippableFactTests(SkippableFactAsserter collector)
        {
            Asserter = collector;
        }

        private SkippableFactAsserter Asserter { get; }

        [Fact]
        public void TestAlwaysRun()
        {
            // This is required to ensure that the type at least gets initialized.
            Assert.True(true);
        }

        [SkippableFact(Skip = "Test is always skipped.")]
        public void SkippableFactSkip()
        {
            Assert.True(false, "This test should always be skipped.");
        }

#if NETCOREAPP2_1
        [SkippableFact]
        [SkipOnRuntimes(Runtimes.CLR)]
        public void ThisTestMustRunOnCoreCLR()
        {
            Asserter.TestRan = true;
        }
#elif NET461
        [SkippableFact]
        [SkipOnRuntimes(Runtimes.CoreCLR)]
        public void ThisTestMustRunOnCLR()
        {
            Asserter.TestRan = true;
        }
#else
#error Target frameworks need to be updated.
#endif

        public class SkippableFactAsserter : IDisposable
        {
            public bool TestRan { get; set; }

            public void Dispose()
            {
                Assert.True(TestRan, "If this assertion fails, a conditional fact wasn't discovered.");
            }
        }
    }
}