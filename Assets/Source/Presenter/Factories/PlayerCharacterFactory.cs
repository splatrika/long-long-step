using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Presenter
{
    [LevelObject]
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

        [SerializeField]
        private Rigidbody _rigidbody;

        [Header("Model")]

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private float _stepLength;

        [SerializeField]
        private int _lifes;

        private ILogger _logger;
        private LevelConfiguration _levelConfiguration;


        [Inject]
        public void Init(ILogger logger, LevelConfiguration levelConfiguration)
        {
            _logger = logger;
            _levelConfiguration = levelConfiguration;
        }


        protected override void OnFirstInit()
        {
            gameObject.AddComponent<PhysicsAdapter>();
        }


        protected override void AdditionalBindings(DiContainer container)
        {
            var configuration = new PlayerCharacterConfiguration
            {
                StepDuration = _levelConfiguration.ActionTime,
                RotationSpeed = _rotationSpeed,
                WaitTime = _levelConfiguration.WaitTime,
                FallTime = _levelConfiguration.WaitTime,
                StepLength = _stepLength,
                Lifes = _lifes,
                Position = transform.position
            };
            container.Bind<PlayerCharacterConfiguration>()
                .FromInstance(configuration);
        }


        protected override void SetupPresenter(PlayerCharacterPresenter presenter)
        {
            presenter.Init(_logger, _firstFoot, _secondFoot, _stepTarget,
                _rigidbody);
        }
    }
}
