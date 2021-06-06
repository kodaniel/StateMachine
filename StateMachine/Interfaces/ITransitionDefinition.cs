using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    public interface ITransitionDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        IStateDefinition<TState, TEvent> Source { get; set; }
        IStateDefinition<TState, TEvent> Target { get; set; }
        IEnumerable<Func<CancellationToken, Task>> Actions { get; }

        public void AddAction(Func<CancellationToken, Task> action);
    }
}
