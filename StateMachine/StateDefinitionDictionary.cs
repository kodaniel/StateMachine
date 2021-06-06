using System;
using System.Collections.Generic;

namespace StateMachine
{
    public class StateDefinitionDictionary<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public readonly Dictionary<TState, IStateDefinition<TState, TEvent>> internalStateDefinitionDictionary;

        public StateDefinitionDictionary()
        {
            internalStateDefinitionDictionary = new Dictionary<TState, IStateDefinition<TState, TEvent>>();
        }
        
        public IStateDefinition<TState, TEvent> this[TState state]
        {
            get
            {
                internalStateDefinitionDictionary.TryGetValue(state, out var result);
                return result;
            }
        }

        public void Add(TState state, IStateDefinition<TState, TEvent> stateDefinition)
        {
            internalStateDefinitionDictionary[state] = stateDefinition;
        }

        public IStateDefinition<TState, TEvent> ForceGet(TState state)
        {
            if (!internalStateDefinitionDictionary.ContainsKey(state))
                internalStateDefinitionDictionary[state] = new StateDefinition<TState, TEvent>(state);
            return internalStateDefinitionDictionary[state];
        }
    }
}
