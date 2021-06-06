using System;
using System.Threading.Tasks;

namespace StateMachine
{
    public interface IStateMachine<TState, TEvent>
		where TState : IComparable
		where TEvent : IComparable
	{
		public TState CurrentState { get; }

		public ITransitionEntry<TState, TEvent> In(TState @state);
		public Task<TransitionResult<TState, TEvent>> Fire(TEvent @event);
	}
}
