using System;
using System.Threading.Tasks;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Core.Utilities.DesignTime
{
    public class DesignDispatcherUtility : IDispatcherUtility
    {
        public bool HasThreadAccess()
        {
            return true;
        }

        public void Run(Action action, int delayms = 0)
        {
        }

        public T Run<T>(Func<T> action, int delayms = 0) where T : class
        {
            return default(T);
        }

        public Task RunAsync(Func<Task> func, int delayms = 0)
        {
            return Task.FromResult(0);
        }

        public Task RunAsync(Action action, int delayms = 0)
        {
            return Task.FromResult(0);
        }

        public Task<T> RunAsync<T>(Func<T> func, int delayms = 0)
        {
            return Task.FromResult(default(T));
        }
    }
}