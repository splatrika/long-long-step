using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILogger>()
                .FromInstance(Debug.unityLogger);
        }
    }
}
