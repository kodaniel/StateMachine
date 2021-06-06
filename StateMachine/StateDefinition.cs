using System;

namespace StateMachine
{
    public class StateDefinition<TState, TEvent> : IStateDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly TransitionDefinitionDictionary<TState, TEvent> transitionDictionary;

        public TState StateId { get; }

        public TransitionDefinitionDictionary<TState, TEvent> Transitions => transitionDictionary;

        public StateDefinition(TState state)
        {
            StateId = state;
            transitionDictionary = new TransitionDefinitionDictionary<TState, TEvent>(this);
        }
    }
}
