using System;

namespace TheDialgaTeam.Core.Tasks
{
    internal class TaskActionState<TState>
    {
        public Action<TState> Action { get; }

        public TState State { get; }

        public TaskActionState(Action<TState> action, TState state)
        {
            Action = action;
            State = state;
        }
    }
}