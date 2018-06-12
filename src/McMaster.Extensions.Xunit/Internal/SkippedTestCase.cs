// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace McMaster.Extensions.Xunit.Internal
{
    /// <summary>
    /// A test case that will be skipped
    /// </summary>
    internal class SkippedTestCase : XunitTestCase
    {
        private string _skipReason;

        /// <inheritdoc />
        [Obsolete(
            "Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public SkippedTestCase()
        {
        }

        /// <inheritdoc />
        public SkippedTestCase(string skipReason, IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod, object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
        {
            _skipReason = skipReason;
        }

        /// <inheritdoc />
        protected override string GetSkipReason(IAttributeInfo factAttribute)
        {
            return _skipReason ?? base.GetSkipReason(factAttribute);
        }

        /// <inheritdoc />
        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);
            _skipReason = data.GetValue<string>(nameof(_skipReason));
        }

        /// <inheritdoc />
        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);
            data.AddValue(nameof(_skipReason), _skipReason);
        }
    }
}