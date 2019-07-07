using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class ChaserInstaller: CarInstaller
    {
        public override void InstallBindings()
        {
            CarId = 0;
            
            base.InstallBindings();
            
            Container.BindInterfacesAndSelfTo<ChaserFacade>().FromNewComponentOnRoot().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CarInactivityRespawn>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChaserDistanceHandler>().AsSingle().NonLazy();
            Container.InstantiateComponent<ChaserCollisionHandler>(CarObject);
        }
    }
}