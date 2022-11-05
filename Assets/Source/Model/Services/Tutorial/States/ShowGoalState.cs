namespace Splatrika.LongLongStep.Model.TutorialStates
{
    public class ShowGoalState : State
    {
        private float _duration;
        private float _timeLeft;
        private IPauseControlService _pauseService { get; }

        public ShowGoalState(
            StatesContext context,
            TutorialConfiguration configuration,
            IPauseControlService pauseService)
            : base(context)
        {
            _pauseService = pauseService;

            _duration = configuration.AccentGoalTime;
        }


        public override void OnStart()
        {
            _pauseService.Pause();
            _timeLeft = _duration;
            Context.RaiseAccentedGoal();
        }


        public override void Update(float deltaTime)
        {
            _timeLeft -= deltaTime;
            if (_timeLeft <= 0)
            {
                _pauseService.Unpause();
                Context.RaiseAccentedPlayer();
                Context.UnlockDefaultLevel();
                Context.SwitchState<FreeGameState>();
            }
        }
    }
}
