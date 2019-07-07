using System.Collections.Generic;
using CarsTest.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class PhotonInputInstaller: Installer<PhotonInputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PhotonView>().WithId("Input").FromNewComponentOnRoot().AsCached();
            Container.Bind<NetworkInput>().FromNewComponentOnRoot().AsSingle();
            Container.BindInterfacesAndSelfTo<PhotonInputInstantiator>().AsSingle();
        }
    }
}