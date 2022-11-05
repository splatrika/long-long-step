using Splatrika.LongLongStep.Architecture;
using UnityEngine;

namespace Splatrika.LongLongStep
{
    [GUI]
    public class TutorialUIFactory
        : SceneObjectFactory<TutorialUI, TutorialUIPresenter>
    {
        [SerializeField]
        private PressButtonHint _leftHint;

        [SerializeField]
        private PressButtonHint _rightHint;

        [SerializeField]
        private GameObject _levelUI;


        protected override void SetupPresenter(TutorialUIPresenter presenter)
        {
            presenter.Init(_leftHint, _rightHint, _levelUI);
        }
    }
}
