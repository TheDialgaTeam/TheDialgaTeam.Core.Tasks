using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheDialgaTeam.Core.Tasks
{
    public static class TaskState
    {
        public static event UnhandledExceptionEventHandler UnhandledException;

        public static Task Run<TState>(TState state, Action<TState> action)
        {
            return Task.Factory.StartNew(action, state, default, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        public static Task Run<TState>(TState state, Action<TState> action, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(action, state, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        public static Task<TResult> Run<TState, TResult>(TState state, Func<TState, TResult> function)
        {
            return Task.Factory.StartNew(function, state, default, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        public static Task<TResult> Run<TState, TResult>(TState state, Func<TState, TResult> function, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(function, state, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        public static void RunAndForget<TState>(TState state, Action<TState> action)
        {
            CatchUnhandledException(Run(state, action));
        }

        public static void RunAndForget<TState>(TState state, Action<TState> action, CancellationToken cancellationToken)
        {
            CatchUnhandledException(Run(state, action, cancellationToken), cancellationToken);
        }

        public static void RunAndForget<TState, TResult>(TState state, Func<TState, TResult> function)
        {
            CatchUnhandledException(Run(state, function));
        }

        public static void RunAndForget<TState, TResult>(TState state, Func<TState, TResult> function, CancellationToken cancellationToken)
        {
            CatchUnhandledException(Run(state, function, cancellationToken), cancellationToken);
        }

        private static void CatchUnhandledException(Task task, CancellationToken cancellationToken = default)
        {
            task.ContinueWith(innerTask =>
            {
                if (innerTask.Exception != null)
                {
                    UnhandledException?.Invoke(innerTask, new UnhandledExceptionEventArgs(innerTask.Exception, false));
                }
            }, cancellationToken);
        }

        private static void CatchUnhandledException<TResult>(Task<TResult> task, CancellationToken cancellationToken = default)
        {
            task.ContinueWith(innerTask =>
            {
                if (innerTask.Exception != null)
                {
                    UnhandledException?.Invoke(innerTask, new UnhandledExceptionEventArgs(innerTask.Exception, false));
                }

                if (innerTask.Result is Task innerResultTask)
                {
                    CatchUnhandledException(innerResultTask, cancellationToken);
                }
            }, cancellationToken);
        }
    }
}