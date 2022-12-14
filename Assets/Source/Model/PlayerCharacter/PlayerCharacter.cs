using System;
using System.Linq;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model.PlayerCharacterStates;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class PlayerCharacter : IPlayerCharacter, IUpdatable
    {
        public float? Progress { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3? StepTarget { get; private set; }
        public float? StepTargetRadians { get; private set; }
        public int Lifes { get; private set; }

        public event Action StepStarted;
        public event Action Wait;
        public event Action Falled;
        public event Action Damaged;
        public event Action Died;
        public event Action<Vector3> Moved;
        public event Action<Vector3?> StepTargetUpdated;
        public event Action<IGround> TouchedGround;
        public event Action Happy;
        public event Action<int> StartedRotation;
        public event Action StoppedRotation;

        private IGround _ground;
        private State _current;
        private State[] _states;
        private bool _rotationAllowed;
        private ILogger _logger { get; }
        private IPhysicsService _physicsService { get; }
        private IPauseService _pauseService { get; }


        public PlayerCharacter(
            ILogger logger,
            IPauseService pauseService,
            IPhysicsService physicsService,
            IRegisterObjectService<IPlayerCharacter> registerService,
            PlayerCharacterConfiguration configuration)
        {
            _logger = logger;
            _pauseService = pauseService;
            _physicsService = physicsService;

            var statesContext = new StatesContext(
                SwitchState, SetProgress, SetPosition, SetStepTarget,
                SetGround, SetStepTargetRadians, Damage,
                () => Progress, () => Position, () => StepTarget, () => Lifes,
                () => _ground, () => _rotationAllowed,
                () => StepStarted?.Invoke(), () => Wait?.Invoke(),
                () => Falled?.Invoke(), () => Happy?.Invoke(),
                direction => StartedRotation?.Invoke(direction),
                () => StoppedRotation?.Invoke());

            CreateStates(statesContext, configuration);
            statesContext.SwitchState<WaitState>();

            Position = configuration.Position;
            Lifes = configuration.Lifes;
            _rotationAllowed = true;

            registerService.Register(this);
        }


        public void Update(float deltaTime)
        {
            if (_pauseService.IsPaused)
            {
                return;
            }
            if (_ground != null)
            {
                SetPosition(_ground.Anchor);
            }
            _current.Update(deltaTime);
        }


        public void StartRotation(int direction)
        {
            _current.StartRotation(direction);
        }


        public void StopRotation()
        {
            _current.StopRotation();
        }


        public void RestrictRotation()
        {
            _current.StopRotation();
            _rotationAllowed = false;
        }


        public void AllowRotation()
        {
            _rotationAllowed = true;
        }


        private void SwitchState(Type stateType)
        {
            var state = _states.SingleOrDefault(x => x.GetType() == stateType);
            if (state == null)
            {
                _logger.LogError(nameof(PlayerCharacter),
                    $"{nameof(SwitchState)}: " +
                    $"There is no state named {stateType.Name}");
                return;
            }
            _current = state;
            _current.OnStart();
        }


        private void Damage(out bool died)
        {
            died = false;
            if (Lifes == 0)
            {
                _logger.LogWarning(nameof(PlayerCharacter),
                    $"{nameof(Damage)}: " +
                    $"The character is already died, but you still trying " +
                    $"to damage them");
                return;
            }
            Lifes--;
            Damaged?.Invoke();
            if (Lifes == 0)
            {
                died = true;
                Died?.Invoke();
                SwitchState(typeof(DiedState));
            }
        }


        private void SetProgress(float? value)
        {
            Progress = value;
        }


        private void SetPosition(Vector3 value)
        {
            Position = value;
            Moved?.Invoke(value);
        }


        private void SetStepTarget(Vector3? value)
        {
            StepTarget = value;
            StepTargetUpdated?.Invoke(value);
        }


        private void SetGround(IGround ground)
        {
            _ground = ground;
            if (_ground != null)
            {
                SetPosition(_ground.Anchor);
            }
            TouchedGround?.Invoke(ground);
        }


        private void SetStepTargetRadians(float? value)
        {
            StepTargetRadians = value;
        }


        private void CreateStates(
            StatesContext context,
            PlayerCharacterConfiguration configuration)
        {
            _states = new State[]
            {
                new WaitState(context, configuration),
                new StepState(context, _logger, _physicsService, configuration),
                new FallState(context, configuration),
                new DiedState(context),
                new HappyState(context)
            };
        }
    }
}
