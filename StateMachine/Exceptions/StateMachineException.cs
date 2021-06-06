using System;

namespace StateMachine
{
    public class StateMachineException : Exception
    {
        public StateMachineException(string message)
            : base(message)
        {
        }
    }
}
