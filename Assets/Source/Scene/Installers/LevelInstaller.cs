using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PauseService>()
                .AsSingle();

            Container
                .BindInterfacesTo<ObjectProviderService<IPlayerCharacter>>()
                .AsSingle();


        public override void Start()
        {
            base.Start();
            // ui initialization will be here
            CreateSceneObjects();
        }


        private void CreateSceneObjects()
        {
            var factories = FindObjectsOfType<SceneObjectFactory>(
                includeInactive: false);
            foreach (var factory in factories)
            {
                factory.Create();
            }
        }
    }
}
