using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelsRepository _levelsRepository;


        public override void InstallBindings()
        {
            var logger = Debug.unityLogger;

            Container.Bind<ILogger>()
                .FromInstance(logger);

            Container.Bind<IScenesService>()
                .To<ScenesService>()
                .AsSingle();

            Container.Bind<ILevelsManagementService>()
                .To<LevelsManagementService>()
                .AsSingle();

            Container.Bind<IUrlService>()
                .To<UrlService>()
                .AsSingle();

            AddApplicationSettings(Container, logger);
            AddLevelsRepository(Container, logger);
        }


        private void AddApplicationSettings(
            DiContainer container, ILogger logger)
        {
            var applicationSettings =
                Resources.Load<ApplictionSettings>("ApplictionSettings");
            if (!applicationSettings)
            {
                logger.LogError(nameof(AppInstaller),
                    "There is no ApplicationSettings.asset in resources " +
                    "folder");
            }
            container.Bind<ApplicationConfiguration>()
                .FromInstance(applicationSettings.GetConfiguration());
        }


        private void AddLevelsRepository(
            DiContainer container, ILogger logger)
        {
            if (!_levelsRepository)
            {
                logger.LogError(nameof(AppInstaller),
                    "There is no level repository assigned to the " +
                    "project context");
                Application.Quit();
            }
            container.Bind<ILevelsRepositoryService>()
                .FromInstance(_levelsRepository);
        }
    }
}
