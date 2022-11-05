using System;
using System.Linq;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model.TutorialStates;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class Tutorial : IUpdatable, IDisposable, ITutorialService
    {
        public float TargetPlayerRotation { get; }
        public int PlayerRotationDirection { get; }

        private State _current;
        private State[] _states;
        private ILevelService _decoratedLevel { get; }
        private IPlayerCharacter _playerCharacter;
        private IObjectProviderService<IPlayerCharacter> _playerProvider { get; }
        private IPauseControlService _pauseService { get; }
        private ILogger _logger { get; }
        private TutorialConfiguration _configuration { get; }
        private bool _freeGame;

        public event Action StartedWaitingForRotate;
        public event Action StopedWaitingForRotate;
        public event Action AccentedGoal;
        public event Action AccentedPlayer;
        public event Action FreeGameUnlocked;
        public event Action Completed;
        public event Action Failed;


        public Tutorial(
            IObjectProviderService<IPlayerCharacter> playerProvider,
            ILogger logger,
            IPauseControlService pauseService,
            TutorialConfiguration configuration)
        {
            _playerProvider = playerProvider;
            _logger = logger;
            _pauseService = pauseService;
            _configuration = new TutorialConfiguration(
                configuration.PlayerRotationDirection,
                configuration.TargetPlayerRotation,
                configuration.AccentGoalTime);
            TargetPlayerRotation = _configuration.TargetPlayerRotation;
            PlayerRotationDirection = _configuration.PlayerRotationDirection;

            _playerProvider.Ready += OnPlayerReady;
        }


        public void Dispose()
        {
            _playerProvider.Ready -= OnPlayerReady;
            if (_playerCharacter != null)
            {
                _playerCharacter.Falled -= OnPlayerFalled;
                _playerCharacter.StepStarted -= OnPlayerStepStarted;
                _playerCharacter.Wait -= OnPlayerWait;
                _playerCharacter.StartedRotation -= OnPlayerStartedRotation;
                _playerCharacter.StoppedRotation -= OnPlayerStoppedRotation;
            }
        }


        public void Update(float deltaTime)
        {
            if (_current == null)
            {
                return;
            }
            _current.Update(deltaTime);
        }


        private void OnPlayerReady(IPlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;

            _playerCharacter.Falled += OnPlayerFalled;
            _playerCharacter.StepStarted += OnPlayerStepStarted;
            _playerCharacter.Wait += OnPlayerWait;
            _playerCharacter.StartedRotation += OnPlayerStartedRotation;
            _playerCharacter.StoppedRotation += OnPlayerStoppedRotation;

            var statesContext = new StatesContext(
                () => TargetPlayerRotation, SwitchState,
                () => StartedWaitingForRotate?.Invoke(),
                () => StopedWaitingForRotate?.Invoke(),
                () => AccentedGoal?.Invoke(),
                () => AccentedPlayer?.Invoke(),
                UnlockFreeGame);

            CreateStates(statesContext);
            SwitchState(typeof(FallIntroductionState));
        }


        private void OnPlayerFalled()
        {
            _current.OnPlayerFalled();
        }


        private void OnPlayerStepStarted()
        {
            _current.OnPlayerStepStarted();
        }


        private void OnPlayerWait()
        {
            _current.OnPlayerWait();
        }


        private void OnPlayerStartedRotation(int _)
        {
            _current.OnPlayerStartedRotation();
        }


        private void OnPlayerStoppedRotation()
        {
            _current.OnPlayerStoppedRotation();
        }


        private void SwitchState(Type type)
        {
            var state = _states.SingleOrDefault(x => x.GetType() == type);
            if (state == null)
            {
                _logger.LogError(nameof(Tutorial),
                    $"{nameof(SwitchState)}: " +
                    $"There is no stete {type.Name}");
                return;
            }
            _current = state;
            _current.OnStart();
        }


        private void UnlockFreeGame()
        {
            FreeGameUnlocked?.Invoke();
        }


        private void CreateStates(StatesContext context)
        {
            _states = new State[]
            {
                new FallIntroductionState(context, _playerCharacter),
                new WaitForRotationState(context, _pauseService),
                new FirstStepState(context, _playerCharacter),
                new ShowGoalState(context, _configuration, _pauseService),
                new FreeGameState(context)
            };
        }
    }
}
