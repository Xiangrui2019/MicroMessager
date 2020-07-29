﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroMessager.DBTools
{
    public static class AsyncHelper
    {
        public static void TryAsyncThreeTimes(Func<Task> steps)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    RunSync(async () => await steps());
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep((i + 1) * 10 * 1000);
                }
            }
        }

        private static readonly TaskFactory TaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => TaskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func)
            => TaskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static Task ForEachParallel<T>(this IEnumerable<T> items, Func<T, Task> function)
            => Task.WhenAll(items
                .Select(t => function(t)));
    }
}