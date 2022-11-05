namespace Splatrika.LongLongStep.Model.TutorialStates
{
    public class FallIntroductionState : State
    {
        private bool _playerFalled;
        private IPlayerCharacter _playerCharacter { get; }


        public FallIntroductionState(
            StatesContext context,
            IPlayerCharacter playerCharacter)
            : base(context)
        {
            _playerCharacter = playerCharacter;
        }


        public override void OnStart()
        {
            _playerCharacter.RestrictRotation();
        }


        public override void OnPlayerFalled()
        {
            _playerFalled = true;
        }


        public override void OnPlayerStepStarted()
        {
            if (_playerFalled)
            {
                _playerCharacter.AllowRotation();
                Context.SwitchState<WaitForRotationState>();
            }
        }
    }
}
