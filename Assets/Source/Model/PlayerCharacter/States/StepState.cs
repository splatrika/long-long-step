using UnityEngine;

namespace Splatrika.LongLongStep.Model.PlayerCharacterStates
{
    public class StepState : State
    {
        private float _rotation;
        private float _stepDuration { get; }
        private float _rotationSpeed { get; }
        private float _stepLength { get; }
        private float _timeLeft;
        private float _rotationDirection;
        private ILogger _logger { get; }
        private IPhysicsService _physicsService { get; }
        private Vector3? _previousAnchor;


        public StepState(
            StatesContext context,
            ILogger logger,
            IPhysicsService physicsService,
            PlayerCharacterConfiguration configuration)
            : base(context)
        {
            _logger = logger;
            _physicsService = physicsService;

            _rotation = 0;
            _stepDuration = configuration.StepDuration;
            _rotationSpeed = configuration.RotationSpeed;
            _stepLength = configuration.StepLength;

            _previousAnchor = null;
        }


        public override sealed void OnStart()
        {
            _rotationDirection = 0;
            _timeLeft = _stepDuration;
            StopRotation();
            UpdateStepTarget();
            Context.Progress = 0;
            Context.RaiseStartStep();
        }


        public override void Update(float deltaTime)
        {
            if (_rotationDirection != 0)
            {
                _rotation += _rotationDirection * _rotationSpeed * deltaTime;
                Context.StepTargetRadians = _rotation;
                UpdateStepTarget();
            }
            else if (Context.Ground?.Anchor != _previousAnchor)
            {
                _previousAnchor = Context.Ground?.Anchor;
                UpdateStepTarget();
            }
            _timeLeft -= deltaTime;
            Context.Progress = 1 - _timeLeft / _stepDuration;
            if (_timeLeft <= 0)
            {
                if (_rotationDirection != 0)
                {
                    Context.RaiseStoppedRotation();
                }
                Context.StepTargetRadians = null;
                if (_physicsService.HasGround(
                    (Vector3)Context.StepTarget, out IGround ground))
                {
                    Context.Ground = ground;
                    Context.StepTarget = null;
                    Context.SwitchState<WaitState>();
                }
                else
                {
                    Context.StepTarget = null;
                    Context.SwitchState<FallState>();
                }
            }
        }


        public override void StartRotation(int direction)
        {
            if (direction != 1 && direction != -1)
            {
                _logger.LogError(nameof(StepState),
                    $"{nameof(StartRotation)}: " +
                    $"1 and -1 can be passed as direction only");
                return;
            }
            if (Context.RotationAllowed)
            {
                _rotationDirection = direction;
                Context.RaiseStartedRotation(direction);
            }
        }


        public override void StopRotation()
        {
            _rotationDirection = 0;
            Context.RaiseStoppedRotation();
        }


        private void UpdateStepTarget()
        {
            var direction = new Vector3(
                Mathf.Sin(_rotation),
                0,
                Mathf.Cos(_rotation));
            Context.StepTarget = Context.Position + direction * _stepLength;
        }
    }
}
