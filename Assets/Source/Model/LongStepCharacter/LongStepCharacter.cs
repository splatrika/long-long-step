using System;
using System.Linq;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model.LongStepCharacterStates;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class LongStepCharacter : IUpdatable
    {
        public float? Progress { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3? StepTarget { get; private set; }
        public int Lifes { get; private set; }

        public event Action StepStarted;
        public event Action Wait;
        public event Action Falled;
        public event Action Damaged;
        public event Action Died;
        public event Action<Vector3> Moved;
        public event Action<Vector3?> StepTargetUpdated;

        private State _current;
        private State[] _states;
        private ILogger _logger { get; }
        private IPhysicsService _physicsService { get; }


        public LongStepCharacter(
            ILogger logger,
            IPhysicsService physicsService,
            LongStepCharacterConfiguration configuration)
        {
            _logger = logger;
            _physicsService = physicsService;

            var statesContext = new StatesContext(
                SwitchState, SetProgress, SetPosition, SetStepTarget, Damage,
                () => Progress, () => Position, () => StepTarget, () => Lifes,
                () => StepStarted?.Invoke(), () => Wait?.Invoke(),
                () => Falled?.Invoke());

            CreateStates(statesContext, configuration);
            statesContext.SwitchState<WaitState>();

            Position = configuration.Position;
            Lifes = configuration.Lifes;
        }


        public void Update(float deltaTime)
        {
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


        private void SwitchState(Type stateType)
        {
            var state = _states.SingleOrDefault(x => x.GetType() == stateType);
            if (state == null)
            {
                _logger.LogError(nameof(LongStepCharacter),
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
                _logger.LogWarning(nameof(LongStepCharacter),
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


        private void CreateStates(
            StatesContext context,
            LongStepCharacterConfiguration configuration)
        {
            _states = new State[]
            {
                new WaitState(context, configuration),
                new StepState(context, _logger, _physicsService, configuration),
                new FallState(context, configuration),
                new DiedState(context)
            };
        }
    }
}
