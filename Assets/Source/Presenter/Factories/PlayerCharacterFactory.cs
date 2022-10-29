using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Presenter
{
    public class PlayerCharacterFactory
        : SceneObjectFactory<PlayerCharacter, PlayerCharacterPresenter>
    {
        [Header("Presenter")]

        [SerializeField]
        private Transform _firstFoot;

        [SerializeField]
        private Transform _secondFoot;

        [SerializeField]
        private Transform _stepTarget;

        [Header("Model")]

        [SerializeField]
        private float _stepDuration;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private float _waitTime;

        [SerializeField]
        private float _fallTime;

        [SerializeField]
        private float _stepLength;

        [SerializeField]
        private int _lifes;

        private ILogger _logger;


        [Inject]
        public void Init(ILogger logger)
        {
            _logger = logger;
        }


        protected override void OnFirstInit()
        {
            gameObject.AddComponent<PhysicsAdapter>();
        }


        protected override void AdditionalBindings(DiContainer container)
        {
            var configuration = new PlayerCharacterConfiguration
            {
                StepDuration = _stepDuration,
                RotationSpeed = _rotationSpeed,
                WaitTime = _waitTime,
                FallTime = _fallTime,
                StepLength = _stepLength,
                Lifes = _lifes,
                Position = transform.position
            };
            container.Bind<PlayerCharacterConfiguration>()
                .FromInstance(configuration);
        }


        protected override void SetupPresenter(PlayerCharacterPresenter presenter)
        {
            presenter.Init(_logger, _firstFoot, _secondFoot, _stepTarget);
        }
    }
}
