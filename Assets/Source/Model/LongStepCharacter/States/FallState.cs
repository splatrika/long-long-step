namespace Splatrika.LongLongStep.Model.LongStepCharacterStates
{
    public class FallState : State
    {
        private float _timeLeft;
        private float _fallTime { get; }


        public FallState(
            StatesContext context,
            LongStepCharacterConfiguration configuration)
            : base(context)
        {
            _fallTime = configuration.FallTime;
        }


        public override void OnStart()
        {
            _timeLeft = _fallTime;
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
