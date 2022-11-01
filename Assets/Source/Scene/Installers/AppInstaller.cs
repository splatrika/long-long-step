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

            if (!_levelsRepository)
            {
                logger.LogError(nameof(AppInstaller),
                    "There is no level repository assigned to the " +
                    "project context");
                Application.Quit();
            }

            Container.Bind<ILevelsRepositoryService>()
                .FromInstance(_levelsRepository);
        }
    }
}
