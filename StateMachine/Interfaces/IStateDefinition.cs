using System;

namespace StateMachine
{
    public interface IStateDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        TState StateId { get; }
        TransitionDefinitionDictionary<TState, TEvent> Transitions { get; }
    }
}
