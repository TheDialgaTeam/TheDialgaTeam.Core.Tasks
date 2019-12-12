using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheDialgaTeam.Core.Tasks
{
    public static class TaskFactoryExtensions
    {
        private class TaskActionState<TState>
        {
            public Action<TState> Action { get; }

            public TState State { get; }

            public TaskActionState(Action<TState> action, TState state)
            {
                Action = action;
                State = state;
            }
        }

        private class TaskFunctionState<TResult, TState>
        {
            public Func<TState, TResult> Function { get; }

            public TState State { get; }

            public TaskFunctionState(Func<TState, TResult> function, TState state)
            {
                Function = function;
                State = state;
            }
        }

        public static System.Threading.Tasks.Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state));
        }

        public static System.Threading.Tasks.Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state, CancellationToken cancellationToken)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state), cancellationToken);
        }

        public static System.Threading.Tasks.Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state, TaskCreationOptions creationOptions)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state), creationOptions);
        }

        public static System.Threading.Tasks.Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state), cancellationToken, creationOptions, scheduler);
        }

        public static Task<TResult> StartNew<TResult, TState>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TResult, TState> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TResult, TState>(function, state));
        }

        public static Task<TResult> StartNew<TResult, TState>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state, CancellationToken cancellationToken)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TResult, TState> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TResult, TState>(function, state), cancellationToken);
        }

        public static Task<TResult> StartNew<TResult, TState>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state, TaskCreationOptions creationOptions)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TResult, TState> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TResult, TState>(function, state), creationOptions);
        }

        public static Task<TResult> StartNew<TResult, TState>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TResult, TState> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TResult, TState>(function, state), cancellationToken, creationOptions, scheduler);
        }
    }
}