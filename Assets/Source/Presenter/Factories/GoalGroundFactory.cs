using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Presenter
{
    public class GoalGroundFactory
        : SceneObjectFactory<GoalGround, StaticGroundPresenter>
    {
        [SerializeField]
        private Transform _anchor;


        protected override void AdditionalBindings(DiContainer container)
        {
            var configuration = new StaticGroundConfiguration(_anchor.position);
            container.Bind<StaticGroundConfiguration>()
                .FromInstance(configuration);
        }
    }
}
