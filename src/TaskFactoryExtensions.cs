using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheDialgaTeam.Core.Tasks
{
    public static class TaskFactoryExtensions
    {
        public static Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state));
        }

        public static Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state, CancellationToken cancellationToken)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state), cancellationToken);
        }

        public static Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state, TaskCreationOptions creationOptions)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state), creationOptions);
        }

        public static Task StartNew<TState>(this TaskFactory taskFactory, Action<TState> action, TState state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskActionState<TState> stateCast)
                    stateCast.Action(stateCast.State);
            }, new TaskActionState<TState>(action, state), cancellationToken, creationOptions, scheduler);
        }

        public static Task<TResult> StartNew<TState, TResult>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TState, TResult> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TState, TResult>(function, state));
        }

        public static Task<TResult> StartNew<TState, TResult>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state, CancellationToken cancellationToken)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TState, TResult> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TState, TResult>(function, state), cancellationToken);
        }

        public static Task<TResult> StartNew<TState, TResult>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state, TaskCreationOptions creationOptions)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TState, TResult> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TState, TResult>(function, state), creationOptions);
        }

        public static Task<TResult> StartNew<TState, TResult>(this TaskFactory taskFactory, Func<TState, TResult> function, TState state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            return taskFactory.StartNew(innerState =>
            {
                if (innerState is TaskFunctionState<TState, TResult> stateCast)
                    return stateCast.Function(stateCast.State);

                return default;
            }, new TaskFunctionState<TState, TResult>(function, state), cancellationToken, creationOptions, scheduler);
        }
    }
}