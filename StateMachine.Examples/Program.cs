using System;
using System.Threading;
using System.Threading.Tasks;
using StateMachine.Examples.Enums;

namespace StateMachine.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();

            Console.ReadLine();
        }

        private IStateMachine<States, MotionControl> _sm;

        private async void Start()
        {
            _sm = new StateMachine<States, MotionControl>(States.Disabled);

            // Configure servo motion control state machine
            _sm.In(States.Disabled).On(MotionControl.MC_Power).GoTo(States.Standstill).Execute(OnServoPowerOn).Execute(OnServoPowerOn).Execute(OnServoPowerOn);
            _sm.In(States.Disabled).On(MotionControl.Error).GoTo(States.ErrorStop).Execute(OnError);
            _sm.In(States.Standstill).On(MotionControl.MC_Home).GoTo(States.Homing);
            _sm.In(States.Standstill).On(MotionControl.MC_MoveVelocity).GoTo(States.ContinuousMotion);
            _sm.In(States.Standstill).On(MotionControl.MC_MoveAbsolute).GoTo(States.DiscreteMotion).Execute(OnMovingDiscrete);
            _sm.In(States.Homing).On(MotionControl.MC_Stop).GoTo(States.Stopping);
            _sm.In(States.Homing).On(MotionControl.Done).GoTo(States.Standstill);
            _sm.In(States.Homing).On(MotionControl.Error).GoTo(States.ErrorStop).Execute(OnError);
            _sm.In(States.ContinuousMotion).On(MotionControl.MC_Stop).GoTo(States.Stopping);
            _sm.In(States.ContinuousMotion).On(MotionControl.MC_MoveAbsolute).GoTo(States.DiscreteMotion).Execute(OnMovingDiscrete);
            _sm.In(States.ContinuousMotion).On(MotionControl.Error).GoTo(States.ErrorStop).Execute(OnError);
            _sm.In(States.DiscreteMotion).On(MotionControl.MC_Stop).GoTo(States.Stopping).Execute(OnStopped);
            _sm.In(States.DiscreteMotion).On(MotionControl.MC_MoveVelocity).GoTo(States.ContinuousMotion);
            _sm.In(States.DiscreteMotion).On(MotionControl.Done).GoTo(States.Standstill);
            _sm.In(States.DiscreteMotion).On(MotionControl.Error).GoTo(States.ErrorStop).Execute(OnError);
            _sm.In(States.Stopping).On(MotionControl.Done).GoTo(States.Standstill);
            _sm.In(States.Stopping).On(MotionControl.Error).GoTo(States.ErrorStop).Execute(OnError);
            _sm.In(States.ErrorStop).On(MotionControl.MC_Reset).GoTo(States.Disabled);

            Console.WriteLine("--- START ---");

            await MoveAndStopServo();

            Console.WriteLine("--- DONE ---");
        }

        private async Task MoveAndStopServo()
        {
            await _sm.Fire(MotionControl.MC_Power);
            await _sm.Fire(MotionControl.MC_MoveAbsolute);
            await Task.Delay(5000);
            await _sm.Fire(MotionControl.MC_Stop);
            await _sm.Fire(MotionControl.Error);
        }

        private async Task OnServoPowerOn(CancellationToken token)
        {
            Console.WriteLine("Enabling servo...");
            await Task.Delay(1000);
            Console.WriteLine("Servo is enabled.");
        }

        private async Task OnMovingDiscrete(CancellationToken token)
        {
            Console.WriteLine("Moving to position.");
        }

        private async Task OnStopped(CancellationToken token)
        {
            Console.WriteLine("Stopping...");
            await Task.Delay(3000);
            Console.WriteLine("Stopped after 3 secs.");
        }

        private async Task OnError(CancellationToken token)
        {
            Console.WriteLine("Servo Error!");
        }
    }
}
