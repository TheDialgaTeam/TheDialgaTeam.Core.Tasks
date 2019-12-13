using System;

namespace TheDialgaTeam.Core.Tasks
{
    internal class TaskFunctionState<TState, TResult>
    {
        public Func<TState, TResult> Function { get; }

        public TState State { get; }

        public TaskFunctionState(Func<TState, TResult> function, TState state)
        {
            Function = function;
            State = state;
        }
    }
}