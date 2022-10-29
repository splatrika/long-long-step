namespace Splatrika.LongLongStep.Model.PlayerCharacterStates
{
    public class WaitState : State
    {
        private float _timeLeft;
        private float _waitTime { get; }


        public WaitState(
            StatesContext context,
            PlayerCharacterConfiguration configuration)
            : base(context)
        {
            _waitTime = configuration.WaitTime;
        }


        public override void OnStart()
        {
            _timeLeft = _waitTime;
            Context.Progress = 0;
            Context.RaiseWait();
        }


        public override void Update(float deltaTime)
        {
            _timeLeft -= deltaTime;
            Context.Progress = 1 - _timeLeft / _waitTime;
            if (_timeLeft <= 0)
            {
                Context.SwitchState<StepState>();
            }
        }
    }
}
