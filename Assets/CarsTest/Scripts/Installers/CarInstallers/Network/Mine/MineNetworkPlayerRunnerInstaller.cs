using System.Collections.Generic;
using CarsTest.Photon;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class MineNetworkPlayerRunnerInstaller: LocalPlayerRunnerInstaller
    {
        protected override void BindChasers()
        {
            Container.Bind<ChaserRegistry>().AsSingle();
            Container.Bind<PlayerController>().FromNewComponentOnRoot().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AIChaserSpawner>().AsSingle();
            Container.BindFactory<ChaserFacade, ChaserFacade.Factory>()
                .WithId("AIChaser")
                .FromPoolableMemoryPool<ChaserFacade, ChaserFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewGameObjectInstaller<MineNetworkAIChaserInstaller>()
                    .WithGameObjectName("AIChaser")
                    .UnderTransformGroup("Chasers"));
            
            Container.BindInterfacesAndSelfTo<PlayerChaserSpawner>().AsSingle();
            Container.BindFactory<ChaserFacade, ChaserFacade.Factory>()
                .WithId("PlayerChaser")
                .FromPoolableMemoryPool<ChaserFacade, ChaserFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(0)
                    .FromSubContainerResolve()
                    .ByNewGameObjectInstaller<MasterNetworkPlayerChaserInstaller>()
                    .WithGameObjectName("PlayerChaser")
                    .UnderTransformGroup("Chasers"));
            
            PhotonCarInstaller.Install(Container, CarObject);
            Container.BindInterfacesAndSelfTo<CarInstantiator>().AsSingle().WithArguments(PlayerType.Runner, false, -1);
        }
    }
}