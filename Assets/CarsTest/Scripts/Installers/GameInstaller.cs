using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using CarsTest.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private readonly Settings _settings = null;
        [Inject] private readonly GameInfo _gameInfo = null;

        public override void InstallBindings()
        {            
            Container.BindFactory<Explosion, Explosion.Factory>()
                .FromPoolableMemoryPool<Explosion, ExplosionPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromComponentInNewPrefab(_settings.explosionPrefab)
                    .UnderTransformGroup("Explosions"));

            Container.Bind<AudioPlayer>().AsSingle();
            
            if (_gameInfo.playerType == PlayerType.Runner)
                GameModesRunnerInstaller.Install(Container, _gameInfo);
            else
                GameModesChaserInstaller.Install(Container, _gameInfo);
            
            BindInput();

            GameSignalsInstaller.Install(Container);

            if (_gameInfo.gameType == GameType.Online)
            {
                PhotonNetwork.IsMessageQueueRunning = true;
                NetworkFactoriesInstaller.Install(Container, _gameInfo);
                NetworkGameModesInstaller.Install(Container, _gameInfo);
            }
            else
            {
                Container.BindInterfacesAndSelfTo<GameModeRunnerManager>().AsSingle();
                Container.BindFactory<RunnerFacade, RunnerFacade.Factory>()
                    .FromSubContainerResolve()
                    .ByNewGameObjectInstaller<LocalPlayerRunnerInstaller>()
                    .WithGameObjectName("Runner");
            }
        }

        private void BindInput()
        {
            if(!_gameInfo.mobileUi)
                Container.Bind<PlayerInputHandler>().To<PlayerStandaloneInputHandler>().AsSingle().MoveIntoDirectSubContainers();
            else
                Container.Bind<PlayerInputHandler>().To<PlayerMobileInputHandler>().AsSingle().MoveIntoDirectSubContainers();
        }

        class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool, Explosion>
        {
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject explosionPrefab;
        }
    }
}