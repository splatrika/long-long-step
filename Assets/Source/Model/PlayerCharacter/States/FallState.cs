namespace Splatrika.LongLongStep.Model.PlayerCharacterStates
{
    public class FallState : State
    {
        private float _timeLeft;
        private float _fallTime { get; }


        public FallState(
            StatesContext context,
            PlayerCharacterConfiguration configuration)
            : base(context)
        {
            _fallTime = configuration.FallTime;
        }


        public override void OnStart()
        {
            _timeLeft = _fallTime;
            Context.Progress = 0;
            Context.RaiseFalled();
        }


        public override void Update(float deltaTime)
        {
            _timeLeft -= deltaTime;
            Context.Progress = 1 - _timeLeft / _fallTime;
            if (_timeLeft <= 0)
            {
                Context.Damage(out bool died);
                if (!died)
                {
                    Context.SwitchState<StepState>();
                }
            }
        }
    }
}
