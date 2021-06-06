using System;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    public class StateMachine<TState, TEvent> : IStateMachine<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StateDefinitionDictionary<TState, TEvent> stateDefinitions;

        public TState CurrentState { get; private set; }
        public CancellationToken CancellationToken { get; set; }

        public StateMachine(TState initialState)
            : this(initialState, new CancellationToken())
        {
        }

        public StateMachine(TState initialState, CancellationToken cancellationToken)
        {
            stateDefinitions = new StateDefinitionDictionary<TState, TEvent>();
            CurrentState = initialState;
            CancellationToken = cancellationToken;
        }

        public ITransitionEntry<TState, TEvent> In(TState @state)
        {
            return new StateBuilder<TState, TEvent>(state, stateDefinitions);
        }

        public async Task<TransitionResult<TState, TEvent>> Fire(TEvent @event)
        {
            var currentStateDefinition = stateDefinitions[CurrentState];
            if (currentStateDefinition is null)
                throw new StateMachineException($"{CurrentState} is not a valid state.");

            var currentTransitionDefinition = currentStateDefinition.Transitions[@event];
            if (currentTransitionDefinition is null)
                return new TransitionResult<TState, TEvent>(false, CurrentState);

            var oldState = CurrentState;
            if (currentTransitionDefinition.Target != null)
                CurrentState = currentTransitionDefinition.Target.StateId;


            foreach (var callbackExecute in currentTransitionDefinition.Actions)
            {
                await callbackExecute(CancellationToken);
            }


            return new TransitionResult<TState, TEvent>(!oldState.Equals(CurrentState), CurrentState);
        }
    }
}
