using System;

namespace StateMachine
{
    public class TransitionResult<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public bool StateChanged { get; }
        public TState NewState { get; }

        public TransitionResult() : this(false, default)
        {
        }

        public TransitionResult(bool stateChanged, TState state)
        {
            StateChanged = stateChanged;
            NewState = state;
        }
    }
}
