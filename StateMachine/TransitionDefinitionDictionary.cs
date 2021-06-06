using System;
using System.Collections.Generic;

namespace StateMachine
{
    public class TransitionDefinitionDictionary<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StateDefinition<TState, TEvent> state;
        private readonly Dictionary<TEvent, ITransitionDefinition<TState, TEvent>> internalTransitionDefinitionDictionary;

        public TransitionDefinitionDictionary(StateDefinition<TState, TEvent> stateDefinition)
        {
            state = stateDefinition;
            internalTransitionDefinitionDictionary = new Dictionary<TEvent, ITransitionDefinition<TState, TEvent>>();
        }

        public ITransitionDefinition<TState, TEvent> this[TEvent @event]
        {
            get
            {
                internalTransitionDefinitionDictionary.TryGetValue(@event, out var result);
                return result;
            }
        }

        public void Add(TEvent @event, ITransitionDefinition<TState, TEvent> transitionDefinition)
        {
            transitionDefinition.Source = state;
            internalTransitionDefinitionDictionary[@event] = transitionDefinition;
        }
    }
}
