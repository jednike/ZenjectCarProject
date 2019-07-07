using CarsTest.Photon;
using Zenject;

namespace CarsTest
{
    public class NetworkFactoriesInstaller: Installer<GameInfo, NetworkFactoriesInstaller>
    {
        private readonly GameInfo _gameInfo;
        public NetworkFactoriesInstaller(GameInfo gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public override void InstallBindings()
        {
            if (_gameInfo.playerType == PlayerType.Runner)
            {
                Container.BindInterfacesAndSelfTo<GameModeRunnerManager>().AsSingle();
                Container.BindInterfacesAndSelfTo<CarBehaviour>().AsSingle()
                    .WhenInjectedInto<MineNetworkPlayerRunnerInstaller>();
                Container.BindFactory<RunnerFacade, RunnerFacade.Factory>()
                    .FromSubContainerResolve()
                    .ByNewGameObjectInstaller<MineNetworkPlayerRunnerInstaller>()
                    .WithGameObjectName("Runner");
            }
            else
            {
                Container.BindInterfacesAndSelfTo<ChaserFactory>().AsSingle();
                Container.BindInterfacesAndSelfTo<GameModeManager>().AsSingle();
                Container.BindFactory<NetworkCarFacade, NetworkCarFacade.Factory>()
                    .WithId("MinePlayer")
                    .FromSubContainerResolve()
                    .ByNewGameObjectInstaller<MineNetworkPlayerChaserInstaller>()
                    .WithGameObjectName("PlayerChaser");
            }

            Container.BindFactory<NetworkCarFacade, NetworkCarFacade.Factory>()
                .WithId("AI")
                .FromSubContainerResolve()
                .ByNewGameObjectInstaller<NetworkChaserInstaller>()
                .WithGameObjectName("AIChaser")
                .UnderTransformGroup("Chasers");
            Container.BindFactory<NetworkCarFacade, NetworkCarFacade.Factory>()
                .WithId("Player")
                .FromSubContainerResolve()
                .ByNewGameObjectInstaller<NetworkChaserInstaller>()
                .WithGameObjectName("Chaser")
                .UnderTransformGroup("Chasers");
            Container.BindFactory<NetworkCarFacade, NetworkCarFacade.Factory>()
                .WithId("Runner")
                .FromSubContainerResolve()
                .ByNewGameObjectInstaller<NetworkRunnerInstaller>()
                .WithGameObjectName("Runner");

            Container.BindInterfacesAndSelfTo<CarFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CarInstantiateHandler>().AsSingle();
        }
    }
}