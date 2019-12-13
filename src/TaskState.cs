using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheDialgaTeam.Core.Tasks
{
    public class TaskState
    {
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
    }
}