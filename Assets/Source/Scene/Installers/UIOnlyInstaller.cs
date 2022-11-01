using System;
using System.Linq;
using System.Reflection;
using Splatrika.LongLongStep.Architecture;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class UIOnlyInstaller : MonoInstaller
    {
        public override sealed void InstallBindings()
        {
        }


        public override sealed void Start()
        {
            base.Start();
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
