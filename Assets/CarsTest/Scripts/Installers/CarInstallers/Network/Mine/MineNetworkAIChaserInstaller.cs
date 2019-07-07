using System.Collections.Generic;
using CarsTest.Photon;
using Photon.Pun;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class MineNetworkAIChaserInstaller: LocalAIChaserInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            PhotonCarInstaller.Install(Container, CarObject);
            Container.BindInterfacesAndSelfTo<CarInstantiator>().AsSingle().WithArguments(PlayerType.Chaser, true, -1);
        }
    }
}