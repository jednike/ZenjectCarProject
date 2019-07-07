using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using CarsTest.Photon;
using Photon.Pun;
using Photon.Realtime;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CarInstaller: Installer<CarInstaller>
    {
        [Inject] private readonly CarsInfo _carsInfos = null;
        protected GameObject CarObject;
        protected int CarId = 0;

        public override void InstallBindings()
        {
            var prefab = _carsInfos.CarInfos[CarId].prefab;
            Container.Bind<CarPhysics>().AsSingle();
            Container.Bind<CarInput>().AsSingle();

            var carView = Container.InstantiatePrefabForComponent<CarView>(prefab);
            Container.BindInstance(carView).AsSingle();
            CarObject = carView.gameObject;
        }
    }
}