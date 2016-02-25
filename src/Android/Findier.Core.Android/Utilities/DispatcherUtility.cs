using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Findier.Core.Utilities.Interfaces;
using Java.Lang;
using Exception = System.Exception;

namespace Findier.Core.Android.Utilities
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-DispatcherWrapper
    public class DispatcherUtility : IDispatcherUtility
    {
        private readonly Func<Activity> _getCurrentActivity;

        public DispatcherUtility(Func<Activity> getCurrentActivity)
        {
            _getCurrentActivity = getCurrentActivity;
        }

        public bool HasThreadAccess()
        {
            return Looper.MainLooper.Thread == Thread.CurrentThread();
        }

        public void Run(Action action, int delayms = 0)
        {
            RunAsync(action, delayms).Wait();
        }

        public T Run<T>(Func<T> action, int delayms = 0) where T : class
        {
            return RunAsync(action, delayms).Result;
        }

        public Task RunAsync(Func<Task> func, int delayms = 0)
        {
            Task.Delay(delayms).Wait();

            if (HasThreadAccess())
            {
                return func();
            }

            var taskCompletion = new TaskCompletionSource<bool>();
            _getCurrentActivity().RunOnUiThread(async () =>
                {
                    try
                    {
                        await func();
                        taskCompletion.SetResult(true);
                    }
                    catch (Exception e)
                    {
                        taskCompletion.SetException(e);
                    }
                });
            return taskCompletion.Task;
        }

        public Task RunAsync(Action action, int delayms = 0)
        {
            Task.Delay(delayms).Wait();

            if (HasThreadAccess())
            {
                action();
                return Task.FromResult(0);
            }

            var taskCompletion = new TaskCompletionSource<bool>();
            _getCurrentActivity().RunOnUiThread(() =>
                {
                    try
                    {
                        action();
                        taskCompletion.SetResult(true);
                    }
                    catch (Exception e)
                    {
                        taskCompletion.SetException(e);
                    }
                });
            return taskCompletion.Task;
        }

        public Task<T> RunAsync<T>(Func<T> func, int delayms = 0)
        {
            Task.Delay(delayms).Wait();

            if (HasThreadAccess())
            {
                return Task.FromResult(func());
            }

            var taskCompletion = new TaskCompletionSource<T>();
            _getCurrentActivity().RunOnUiThread(() =>
                {
                    try
                    {
                        taskCompletion.SetResult(func());
                    }
                    catch (Exception e)
                    {
                        taskCompletion.SetException(e);
                    }
                });
            return taskCompletion.Task;
        }

        public Task<T> RunAsync<T>(Func<Task<T>> func, int delayms = 0)
        {
            Task.Delay(delayms).Wait();

            if (HasThreadAccess())
            {
                return func();
            }

            var taskCompletion = new TaskCompletionSource<T>();
            _getCurrentActivity().RunOnUiThread(async () =>
                {
                    try
                    {
                        taskCompletion.SetResult(await func());
                    }
                    catch (Exception e)
                    {
                        taskCompletion.SetException(e);
                    }
                });
            return taskCompletion.Task;
        }
    }
}