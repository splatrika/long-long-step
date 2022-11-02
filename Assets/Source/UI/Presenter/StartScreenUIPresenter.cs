using Splatrika.LongLongStep.Architecture;
using TMPro;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    public class StartScreenUIPresenter : Presenter<StartScreenUI>
    {
        private Button _play;
        private Button _gitHub;
        private TextMeshProUGUI _version;


        public void Init(
            Button play,
            Button gitHub,
            TextMeshProUGUI version,
            string versionFormat)
        {
            _play = play;
            _gitHub = gitHub;
            _version = version;

            _play.onClick.AddListener(OnPlayClicked);
            _gitHub.onClick.AddListener(OnGitHubClicked);

            _version.text = string.Format(versionFormat, TypedModel.Version);
        }


        private void OnPlayClicked()
        {
            TypedModel.Play();
        }


        private void OnGitHubClicked()
        {
            TypedModel.OpenGitHub();
        }
    }
}
