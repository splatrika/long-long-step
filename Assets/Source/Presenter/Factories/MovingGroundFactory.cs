using System.Collections;
using System.Collections.Generic;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Presenter
{
    [LevelObject]
    public class MovingGroundFactory
        : SceneObjectFactory<MovingGround, MovingGroundPresenter>
    {
        [Header("Presenter")]

        [SerializeField]
        private Transform _anchor;

        [Header("Model")]

        [SerializeField]
        private Transform _pointA;

        [SerializeField]
        private Transform _pointB;

        private LevelConfiguration _levelConfiguration;


        [Inject]
        public void Init(LevelConfiguration levelConfiguration)
        {
            _levelConfiguration = levelConfiguration;
        }


        protected override void AdditionalBindings(DiContainer container)
        {
            var configuration = new MovingGroundConfiguration(
                pointA: _pointA.position,
                pointB: _pointB.position,
                movementDuration: _levelConfiguration.ActionTime,
                waitTime: _levelConfiguration.WaitTime,
                waitAtStart: true);
            container.Bind<MovingGroundConfiguration>()
                .FromInstance(configuration);
        }


        protected override void SetupPresenter(MovingGroundPresenter presenter)
        {
            presenter.Init(transform, _anchor);
        }
    }
}
