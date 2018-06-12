// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace System.Threading.Tasks
{
    /// <summary>
    ///     Useful extension for writing async tests.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Timeout this task after waigin for a set amount of time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="timeout">How long to wait before failing the test</param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = default)
        {
            // Don't create a timer if the task is already completed
            if (task.IsCompleted) return await task;

            var cts = new CancellationTokenSource();
            if (task == await Task.WhenAny(task, Task.Delay(timeout, cts.Token)))
            {
                cts.Cancel();
                return await task;
            }

            throw new TimeoutException(
                CreateMessage(timeout, filePath, lineNumber));
        }

        /// <summary>
        ///     Timeout this task after waigin for a set amount of time.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="timeout">How long to wait before failing the test</param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = default)
        {
            // Don't create a timer if the task is already completed
            if (task.IsCompleted)
            {
                await task;
                return;
            }

            var cts = new CancellationTokenSource();
            if (task == await Task.WhenAny(task, Task.Delay(timeout, cts.Token)))
            {
                cts.Cancel();
                await task;
            }
            else
            {
                throw new TimeoutException(CreateMessage(timeout, filePath, lineNumber));
            }
        }

        private static string CreateMessage(TimeSpan timeout, string filePath, int lineNumber)
        {
            return string.IsNullOrEmpty(filePath)
                ? $"The operation timed out after reaching the limit of {timeout.TotalMilliseconds}ms."
                : $"The operation at {filePath}:{lineNumber} timed out after reaching the limit of {timeout.TotalMilliseconds}ms.";
        }
    }
}