using System.Collections.Generic;
using CarsTest.Photon;
using Photon.Pun;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class NetworkCarInstaller: CarInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NetworkCarFacade>().FromNewComponentOnRoot().AsSingle();
            Container.Bind<CarDeathHandler>().AsSingle();
            base.InstallBindings();
            PhotonCarInstaller.Install(Container, CarObject);
        }
    }
    public class NetworkChaserInstaller: NetworkCarInstaller
    {
        public override void InstallBindings()
        {
            CarId = 0;
            base.InstallBindings();
        }
    }
    public class NetworkRunnerInstaller: NetworkCarInstaller
    {
        public override void InstallBindings()
        {
            CarId = 1;
            base.InstallBindings();
        }
    }
}