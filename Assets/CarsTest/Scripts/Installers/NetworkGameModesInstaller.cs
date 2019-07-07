using System;
using CarsTest.Photon;
using Photon.Pun;
using Zenject;

namespace CarsTest
{
    public class NetworkGameModesInstaller: Installer<GameInfo, NetworkGameModesInstaller>
    {
        private readonly GameInfo _gameInfo;
        public NetworkGameModesInstaller(GameInfo gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public override void InstallBindings()
        {
            Container.Bind<PhotonView>().FromNewComponentOnRoot().AsCached();
            Container.BindInterfacesAndSelfTo<GameModeObserver>().AsSingle();

            if (_gameInfo.playerType == PlayerType.Runner)
            {
                Container.BindInterfacesAndSelfTo<ManagerInstantiator>().AsSingle();
                Container.BindInterfacesTo<NetworkGameModeManager>().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<ManagerHandler>().AsSingle();
                Container.BindInterfacesAndSelfTo<ManagerFactory>().AsSingle();
            }

            switch (_gameInfo.currentGameMode)
            {
                case GameMode.Checkpoint:
                    Container.Bind<NetworkGameModeObserver>().To<CheckpointsModeNetworkBehaviour>().FromNewComponentOnRoot().AsCached();            
                    break;
                case GameMode.Survive:
                    Container.Bind<NetworkGameModeObserver>().To<SurviveModeNetworkBehaviour>().FromNewComponentOnRoot().AsCached();            
                    break;
                case GameMode.Destroy:
                    Container.Bind<NetworkGameModeObserver>().To<DestroyModeNetworkBehaviour>().FromNewComponentOnRoot().AsCached();            
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}