// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace McMaster.Extensions.Xunit.Internal
{
    internal interface IEnvironmentVariable
    {
        string Get(string name);
    }
}