// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace McMaster.Extensions.Xunit
{
    public class DockerTests
    {
        [SkippableFact]
        [SkipIfNotDocker]
        [Trait("Docker", "true")]
        public void DoesNotRunOnWindows()
        {
            Assert.False(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
        }
    }
}
