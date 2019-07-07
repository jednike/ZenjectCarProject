using Zenject;

namespace CarsTest
{
    public class PlayerControlledInstaller: Installer<PlayerControlledInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputManager>().AsSingle().NonLazy();
            Container.Bind<PlayerCameraHandler>().AsSingle().NonLazy();
        }
    }
}