namespace Splatrika.LongLongStep.Model.TutorialStates
{
    public class WaitForRotationState : State
    {
        private IPauseControlService _pauseService;


        public WaitForRotationState(
            StatesContext context,
            IPauseControlService pauseService)
            : base(context)
        {
            _pauseService = pauseService;
        }


        public override void OnStart()
        {
            _pauseService.Pause();
            Context.RaiseStartedWaitingForRotate();
        }


        public override void OnPlayerStartedRotation()
        {
            _pauseService.Unpause();
            Context.RaiseStoppedWaitingForRotate();
            Context.SwitchState<FirstStepState>();
        }
    }
}
