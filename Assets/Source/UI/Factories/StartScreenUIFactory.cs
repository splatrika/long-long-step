using Splatrika.LongLongStep.Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    [GUI]
    public class StartScreenUIFactory
        : SceneObjectFactory<StartScreenUI, StartScreenUIPresenter>
    {
        [SerializeField]
        private Button _play;

        [SerializeField]
        private Button _gitHub;

        [SerializeField]
        private TextMeshProUGUI _version;

        [SerializeField]
        private string _versionFormat = "ver. {0}";


        protected override void SetupPresenter(StartScreenUIPresenter presenter)
        {
            presenter.Init(_play, _gitHub, _version, _versionFormat);
        }
    }
}
