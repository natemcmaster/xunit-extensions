Xunit Extensions
================

[![Travis build status][travis-badge]](https://travis-ci.org/natemcmaster/xunit-extensions/branches)
[![AppVeyor build status][appveyor-badge]](https://ci.appveyor.com/project/natemcmaster/xunit-extensions/branch/master)

[travis-badge]: https://img.shields.io/travis/natemcmaster/xunit-extensions/master.svg?label=travis&style=flat-square
[appveyor-badge]: https://img.shields.io/appveyor/ci/natemcmaster/xunit-extensions/master.svg?label=appveyor&style=flat-square

[![NuGet][main-nuget-badge]][main-nuget] [![MyGet][main-myget-badge]][main-myget]

[main-nuget]: https://www.nuget.org/packages/McMaster.Extensions.Xunit/
[main-nuget-badge]: https://img.shields.io/nuget/v/McMaster.Extensions.Xunit.svg?style=flat-square&label=nuget
[main-myget]: https://www.myget.org/feed/natemcmaster/package/nuget/McMaster.Extensions.Xunit
[main-myget-badge]: https://img.shields.io/www.myget/natemcmaster/vpre/McMaster.Extensions.Xunit.svg?style=flat-square&label=myget


This repo contains a class library of utilties and helpers for writing tests with XUnit.NET.

## Getting started

Install this as a NuGet package using your favorite IDE.

```
Install-Package McMaster.Extensions.Xunit
dotnet add package McMaster.Extensions.Xunit
```

## Dynamically skip tests

In many cases, it's useful to automatically skip tests, based on conditions that cannot
be determined at compile-time.

Test conditions you can use include

* SkipOnOS (skip on certain operating systems)
* SkipInEnvironment (skip if certain environment variables are set)
* SkipOnRuntimes (only run tests on Mono, .NET Core, or .NET Framework)
* SkipIfNotDocker (only run tests if running inside a Docker container)
* Your own (if you write a test that implements `ITestCondition`)
```c#
using McMaster.Extensions.Xunit;

public class MyTests
{
    [SkippableFact]
    [SkipOnOS(OS.Linux | OS.MacOS)
    public void RunCommandPrompt()
    {
        Process.Start("cmd.exe", "/c dir").WaitForExit();
    }
    
    [SkippableFact]
    [SkipUnlessIsNoon]
    public void Test1() { }
    
    // Implement your own conditional logic by implementing ITestCondition
    public class SkipUnlessIsNoonAttribute : Attribute, ITestCondition
    {
        public bool IsMet => DateTime.Now.Hours == 12;
        public string SkipReason { get; } = "This test can only run at noon."
    }
}
```

## Test in different cultures

Ensure tests run with a particular culture set.

```c#
using McMaster.Extensions.Xunit;

public class I18nTests
{
    [Fact]
    [UseCulture("fr-FR")
    public void TestInFrench()
    {
    }
}
```

## Misc

Other useful helpers are included.

Task.TimeoutAfter - prevent async from running too long

```c#
[Fact]
public async Task Test()
{
    await thing.TimeoutAfter(TimeSpan.FromMinutes(4));
}
```