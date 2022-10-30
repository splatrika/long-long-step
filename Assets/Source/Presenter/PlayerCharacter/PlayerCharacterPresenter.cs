using System;
using System.Linq;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter
{
    public class PlayerCharacterPresenter : Presenter<PlayerCharacter>
    {
        private ILogger _logger;
        private Transform _stepTarget;
        private Transform[] _foots;
        private int _steppingFoot;
        private State _current;
        private State[] _states;

#if DEBUG
        private Vector3 _debugStepTarget;
        private string _debugPresenterState;
        private int _debugLifes;
        private Vector3 _debugPosition;
#endif


        public void Init(
            ILogger logger,
            Transform firstFoot,
            Transform secondFoot,
            Transform stepTarget)
        {
            _logger = logger;
            _stepTarget = stepTarget;
            _foots = new Transform[] { firstFoot, secondFoot };

            _steppingFoot = 0;

            var statesContext = new StatesContext(
                SwitchState, () => TypedModel,
                () => _foots, () => stepTarget, () => _steppingFoot,
                SetSteppingFoot);

            CreateStates(statesContext);

            statesContext.SwitchState<WaitState>();
        }


        protected override sealed void OnConstruct(PlayerCharacter model)
        {
            model.StepStarted += OnStepStarted;
            model.Wait += OnWait;
            model.Falled += OnFalled;
            model.Died += OnDied;
            model.StepTargetUpdated += OnStepTargetUpdated;
            model.Moved += OnMoved;
        }


        protected override sealed void OnDispose()
        {
            TypedModel.StepStarted -= OnStepStarted;
            TypedModel.Wait -= OnWait;
            TypedModel.Falled -= OnFalled;
            TypedModel.Died -= OnDied;
            TypedModel.StepTargetUpdated -= OnStepTargetUpdated;
            TypedModel.Moved -= OnMoved;
        }


        protected override void OnUpdate(float deltaTime)
        {
#if DEBUG
            UpdateDebug();
#endif
            if (Input.GetKeyDown("a"))
            {
                TypedModel.StartRotation(-1);
            }
            if (Input.GetKeyDown("d"))
            {
                TypedModel.StartRotation(1);
            }
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                TypedModel.StopRotation();
            }
            _current.OnUpdate(deltaTime);
        }


        private void OnStepStarted()
        {
            _current.OnStepStarted();
        }


        private void OnWait()
        {
            _current.OnWait();
        }


        private void OnFalled()
        {
            _current.OnFalled();
        }


        private void OnDied()
        {
            _current.OnDied();
        }


        private void OnStepTargetUpdated(Vector3? value)
        {
            _current.OnStepTargetUpdated(value);
        }


        private void OnMoved(Vector3 position)
        {
            _current.OnMoved(position);
        }


        private void SetSteppingFoot(int value)
        {
            _steppingFoot = value;
        }


        private void SwitchState(Type stateType)
        {
            var state = _states
                .SingleOrDefault(x => x.GetType() == stateType);
            if (state == null)
            {
                _logger.LogError(nameof(PlayerCharacterPresenter),
                    $"{nameof(SwitchState)}: " +
                    $"There is no state {stateType.Name}");
            }
            _current = state;
            _current.OnStart();
        }


        private void CreateStates(StatesContext context)
        {
            _states = new State[]
            {
                new WaitState(context),
                new StepState(context),
                new FallState(context)
            };
        }


#if DEBUG
        private void UpdateDebug()
        {
            _debugStepTarget = TypedModel.StepTarget ?? Vector3.zero;
            _debugPresenterState = _current.GetType().Name;
            _debugLifes = TypedModel.Lifes;
            _debugPosition = TypedModel.Position;
        }
#endif
    }
}
