using System;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    public interface ITransitionEntry<TState, TEvent>
	{
		public ITransitionOn<TState, TEvent> On(TEvent @event);
	}

	public interface ITransitionOn<TState, TEvent>
	{
		public ITransitionOn<TState, TEvent> GoTo(TState @state);
		public ITransitionOn<TState, TEvent> Execute(Func<CancellationToken, Task> callback);
	}
}
