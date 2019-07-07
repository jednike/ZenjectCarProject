using CarsTest.Photon;

namespace CarsTest
{
    public class MasterNetworkPlayerChaserInstaller: LocalPlayerChaserInstaller
    {
        public override void InstallBindings()
        {            
            Container.Bind<CarDeathHandler>().To<PlayerChaserDeathHandler>().AsSingle();
            
            base.InstallBindings();
            
            PhotonInputInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<InputInstantiateHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputFactory>().AsSingle();
            
            PhotonCarInstaller.Install(Container, CarObject);
            Container.BindInterfacesAndSelfTo<CarInstantiator>().AsSingle().WithArguments(PlayerType.Chaser, false, PlayerController.OwnerId);
        }
    }
}