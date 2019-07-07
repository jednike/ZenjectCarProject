using CarsTest.Photon;
using Photon.Pun;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class PhotonCarInstaller: Installer<GameObject, PhotonCarInstaller>
    {
        private readonly GameObject _carObject;
        public PhotonCarInstaller(GameObject carObject)
        {
            _carObject = carObject;
        }

        public override void InstallBindings()
        {
            Container.Bind<PhotonView>().WithId("Car").FromNewComponentOn(_carObject).AsCached();
            Container.Bind<SmoothSyncPUN2>().FromNewComponentOn(_carObject).AsSingle();
            Container.BindInterfacesAndSelfTo<PhotonCarInstantiator>().AsSingle();
        }
    }
}