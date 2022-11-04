using System;

namespace Splatrika.LongLongStep.Model
{
    public class Level : ILevelService, IDisposable
    {
        private State _state;
        private IPlayerCharacter _playerCharacter;
        private IObjectProviderService<IPlayerCharacter> _playerProvider;
        private ITimeService _timeService;
        private bool _playerFalled;

        public event Action Completed;
        public event Action Failed;


        public Level(
            IObjectProviderService<IPlayerCharacter> playerProvider,
            ITimeService timeService)
        {
            _playerProvider = playerProvider;
            _timeService = timeService;

            _playerProvider.Ready += OnPlayerReady;

            _state = State.Gameplay;
        }


        private void OnPlayerTouchedGround(IGround ground)
        {
            if (ground is IGoalGround && _state == State.Gameplay)
            {
                _state = State.Complete;
                Completed?.Invoke();
            }
        }


        private void OnPlayerDied()
        {
            if (_state != State.Gameplay)
            {
                return;
            }
            _state = State.Fail;
            Failed.Invoke();
        }


        private void OnPlayerFalled()
        {
            _playerFalled = true;
        }


        private void OnPlayerStartedStep()
        {
            if (_playerFalled)
            {
                _playerFalled = false;
                _timeService.RevertToPreviousAction();
            }
        }


        private void OnPlayerReady(IPlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;
            _playerCharacter.Died += OnPlayerDied;
            _playerCharacter.TouchedGround += OnPlayerTouchedGround;
            _playerCharacter.Falled += OnPlayerFalled;
            _playerCharacter.StepStarted += OnPlayerStartedStep;
        }


        public void Dispose()
        {
            if (_playerCharacter != null)
            {
                _playerCharacter.Died -= OnPlayerDied;
                _playerCharacter.TouchedGround -= OnPlayerTouchedGround;
                _playerCharacter.Falled -= OnPlayerFalled;
                _playerCharacter.StepStarted -= OnPlayerStartedStep;
            }
            _playerProvider.Ready -= OnPlayerReady;
        }


        public enum State
        {
            Gameplay,
            Fail,
            Complete
        }
    }
}
