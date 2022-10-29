using Splatrika.LongLongStep.Model;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PauseService>()
                .AsSingle();
        }
    }
}
