namespace CarsTest
{
    public class LocalPlayerChaserInstaller: ChaserInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CarBehaviour>().AsSingle();
            base.InstallBindings();
            PlayerControlledInstaller.Install(Container);
        }
    }
}