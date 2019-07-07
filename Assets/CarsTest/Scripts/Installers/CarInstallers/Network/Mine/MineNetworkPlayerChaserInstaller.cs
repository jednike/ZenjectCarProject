using System.Collections.Generic;
using CarsTest.Photon;
using Photon.Pun;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class MineNetworkPlayerChaserInstaller: NetworkCarInstaller
    {
        public override void InstallBindings()
        {
            CarId = 0;
            
            base.InstallBindings();
            PlayerControlledInstaller.Install(Container);
            
            PhotonInputInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<InputInstantiator>().AsSingle();
        }
    }
}