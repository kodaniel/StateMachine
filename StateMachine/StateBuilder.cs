using System;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    public class StateBuilder<TState, TEvent> : 
        ITransitionEntry<TState, TEvent>,
        ITransitionOn<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StateDefinitionDictionary<TState, TEvent> stateDefinitionDictionary;
        private readonly IStateDefinition<TState, TEvent> stateDefinition;
        private ITransitionDefinition<TState, TEvent> currentTransitionDefinition;

        public StateBuilder(TState state, StateDefinitionDictionary<TState, TEvent> stateDefinitionDictionary)
        {
            this.stateDefinitionDictionary = stateDefinitionDictionary;
            
            // Add StateDefinition implicitly if not exist in the dictionary yet.
            this.stateDefinition = stateDefinitionDictionary.ForceGet(state);
        }

        ITransitionOn<TState, TEvent> ITransitionEntry<TState, TEvent>.On(TEvent @event)
        {
            CreateTransition(@event);

            return this;
        }

        ITransitionOn<TState, TEvent> ITransitionOn<TState, TEvent>.GoTo(TState state)
        {
            currentTransitionDefinition.Target = stateDefinitionDictionary.ForceGet(state);

            return this;
        }

        ITransitionOn<TState, TEvent> ITransitionOn<TState, TEvent>.Execute(Func<CancellationToken, Task> callback)
        {
            currentTransitionDefinition.AddAction(callback);

            return this;
        }

        private void CreateTransition(TEvent @event)
        {
            currentTransitionDefinition = new TransitionDefinition<TState, TEvent>();
            stateDefinition.Transitions.Add(@event, currentTransitionDefinition);
        }
    }
}
