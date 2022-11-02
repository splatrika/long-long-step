using Splatrika.LongLongStep.Model;

namespace Splatrika.LongLongStep
{
    public class StartScreenUI
    {
        public string Version { get; }

        private string _githubUrl { get; }
        private IUrlService _urlService { get; }
        private IScenesService _scenesService { get; }


        public StartScreenUI(
            IUrlService urlService,
            IScenesService scenesService,
            ApplicationConfiguration configuration)
        {
            _urlService = urlService;
            _scenesService = scenesService;

            _githubUrl = configuration.GitHubUrl;
            Version = configuration.Version;
        }


        public void Play()
        {
            _scenesService.Load(Scenes.LevelSelect);
        }


        public void OpenGitHub()
        {
            _urlService.Open(_githubUrl);
        }
    }
}
