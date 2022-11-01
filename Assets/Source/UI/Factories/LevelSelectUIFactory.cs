using Splatrika.LongLongStep.Architecture;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Splatrika.LongLongStep
{
    [GUI]
    public class LevelSelectUIFactory
        : SceneObjectFactory<LevelSelectUI, LevelSelectUIPresenter>
    {
        [SerializeField]
        private LevelButton[] _levelButtons;

        [SerializeField]
        private Button _previousPage;

        [SerializeField]
        private Button _nextPage;


        protected override sealed void AdditionalBindings(DiContainer container)
        {
            var configuration = new LevelSelectUIConfiguration(
                levelsOnPage: _levelButtons.Length);
            container.Bind<LevelSelectUIConfiguration>()
                .FromInstance(configuration);
        }


        protected override sealed void SetupPresenter(
            LevelSelectUIPresenter presenter)
        {
            presenter.Init(_levelButtons, _previousPage, _nextPage);
        }
    }
}
