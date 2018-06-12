// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Xunit;
using Xunit.Abstractions;

namespace McMaster.Extensions.Xunit
{
    public class SkippableTheoryTests : IClassFixture<SkippableTheoryTests.SkippableTheoryAsserter>
    {
        public SkippableTheoryTests(SkippableTheoryAsserter asserter)
        {
            Asserter = asserter;
        }

        public SkippableTheoryAsserter Asserter { get; }

        [SkippableTheory(Skip = "Test is always skipped.")]
        [InlineData(0)]
        public void SkippableTheorySkip(int arg)
        {
            Assert.True(false, "This test should always be skipped.");
        }

        private static int _SkippableTheoryRuns;

        [SkippableTheory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2, Skip = "Skip these data")]
        public void SkippableTheoryRunOncePerDataLine(int arg)
        {
            _SkippableTheoryRuns++;
            Assert.True(_SkippableTheoryRuns <= 2, $"Theory should run 2 times, but ran {_SkippableTheoryRuns} times.");
        }

        [SkippableTheory]
        [Trait("Color", "Blue")]
        [InlineData(1)]
        public void ConditionalTheoriesShouldPreserveTraits(int arg)
        {
            Assert.True(true);
        }

        [SkippableTheory(Skip = "Skip this")]
        [MemberData(nameof(GetInts))]
        public void ConditionalTheoriesWithSkippedMemberData(int arg)
        {
            Assert.True(false, "This should never run");
        }

        private static int _conditionalMemberDataRuns;

        [SkippableTheory]
        [InlineData(4)]
        [MemberData(nameof(GetInts))]
        public void ConditionalTheoriesWithMemberData(int arg)
        {
            _conditionalMemberDataRuns++;
            Assert.True(_SkippableTheoryRuns <= 3,
                $"Theory should run 2 times, but ran {_conditionalMemberDataRuns} times.");
        }

        public static TheoryData<int> GetInts
            => new TheoryData<int> {0, 1};

        [SkippableTheory]
        [SkipOnOS(OS.Windows)]
        [SkipOnOS(OS.MacOS)]
        [SkipOnOS(OS.Linux)]
        [MemberData(nameof(GetActionTestData))]
        public void SkippableTheoryWithFuncs(Func<int, int> func)
        {
            Assert.True(false, "This should never run");
        }

        [Fact]
        public void TestAlwaysRun()
        {
            // This is required to ensure that this type at least gets initialized.
            Assert.True(true);
        }

#if NETCOREAPP2_1
        [SkippableTheory]
        [SkipOnRuntimes(Runtimes.NETFramework)]
        [MemberData(nameof(GetInts))]
        public void ThisTestMustRunOnCoreCLR(int value)
        {
            Asserter.TestRan = true;
        }

#elif NET461
        [SkippableTheory]
        [SkipOnRuntimes(Runtimes.NETCore)]
        [MemberData(nameof(GetInts))]
        public void ThisTestMustRunOnCLR(int value)
        {
            Asserter.TestRan = true;
        }
#else
#error Target frameworks need to be updated.
#endif

        public static TheoryData<Func<int, int>> GetActionTestData
            => new TheoryData<Func<int, int>>
            {
                i => i * 1
            };

        public class SkippableTheoryAsserter : IDisposable
        {
            public bool TestRan { get; set; }

            public void Dispose()
            {
                Assert.True(TestRan, "If this assertion fails, a skippable theory wasn't discovered.");
            }
        }

        [SkippableTheory]
        [MemberData(nameof(SkippableData))]
        public void WithSkipableData(Skippable skippable)
        {
            Assert.Null(skippable.Skip);
            Assert.Equal(1, skippable.Data);
        }

        public static TheoryData<Skippable> SkippableData => new TheoryData<Skippable>
        {
            new Skippable {Data = 1},
            new Skippable {Data = 2, Skip = "This row should be skipped."}
        };

        public class Skippable : IXunitSerializable
        {
            public int Data { get; set; }
            public string Skip { get; set; }

            public void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue(nameof(Data), Data, typeof(int));
            }

            public void Deserialize(IXunitSerializationInfo info)
            {
                Data = info.GetValue<int>(nameof(Data));
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }
    }
}