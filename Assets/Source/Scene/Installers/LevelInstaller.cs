using System;
using System.Linq;
using System.Reflection;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private float _actionTime = 5;

        [SerializeField]
        private float _waitTime = 2;


        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PauseService>()
                .AsSingle();

            Container
                .BindInterfacesTo<ObjectProviderService<IPlayerCharacter>>()
                .AsSingle();

            Container.Bind<ITimeService>()
                .To<TimeService>()
                .AsSingle();

            Container.BindInterfacesTo<Level>()
                .AsSingle();

            var levelConfiguration = new LevelConfiguration(
                _actionTime, _waitTime);
            Container.Bind<LevelConfiguration>()
                .FromInstance(levelConfiguration);
        }


        public override void Start()
        {
            base.Start();
            CreateSceneObjects();
            CreateGUI();
        }


        private void CreateSceneObjects()
        {
            CallFactoriesWithAttribute<LevelObjectAttribute>();
        }


        private void CreateGUI()
        {
            CallFactoriesWithAttribute<GUIAttribute>();
        }


        private void CallFactoriesWithAttribute<T>() where T : Attribute
        {
            var factories = FindObjectsOfType<SceneObjectFactory>(
                includeInactive: false);
            foreach (var factory in factories)
            {
                if (factory.GetType().GetCustomAttributes().Any(x => x is T))
                {
                    factory.Create();
                }
            }
        }
    }
}
