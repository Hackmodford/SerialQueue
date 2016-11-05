using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    public class SerialQueue
    {
        SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async Task<T> Enqueue<T>(Func<T> function)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await Task.Run(function);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Enqueue(Action action)
        {
            await _semaphore.WaitAsync();
            try
            {
                await Task.Run(action);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}

