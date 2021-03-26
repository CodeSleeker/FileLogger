using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Logger
{
    public class Awaiter
    {
        public static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        public static readonly Dictionary<string, SemaphoreSlim> Semaphores = new Dictionary<string, SemaphoreSlim>();
        public static async Task<T> ResultAsync<T>(string key, Func<Task<T>> task, int count = 1)
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(count, count));
            }
            finally
            {
                SemaphoreSlim.Release();
            }
            var semaphore = Semaphores[key];
            await semaphore.WaitAsync();
            try
            {
                return await task();
            }
            finally
            {
                semaphore.Release();
            }
        }
        public static async Task Async(string key, Func<Task> task, int count = 1)
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(count, count));
            }
            finally
            {
                SemaphoreSlim.Release();
            }
            var semaphore = Semaphores[key];
            await semaphore.WaitAsync();
            try
            {
                await task();
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
