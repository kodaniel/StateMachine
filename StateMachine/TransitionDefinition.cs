using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    public class TransitionDefinition<TState, TEvent> : ITransitionDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly List<Func<CancellationToken, Task>> _actions;

        public IStateDefinition<TState, TEvent> Source { get; set; }
        public IStateDefinition<TState, TEvent> Target { get; set; }

        public IEnumerable<Func<CancellationToken, Task>> Actions => _actions;

        public TransitionDefinition()
        {
            _actions = new List<Func<CancellationToken, Task>>();
        }

        public void AddAction(Func<CancellationToken, Task> action)
        {
            _actions.Add(action);
        }
    }
}
