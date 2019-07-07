using System;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class ChaserFactory: EventCarFactory
    {
        private readonly NetworkCarFacade.Factory _playerChaserFactory;
        public ChaserFactory(SignalBus signalBus, [Inject(Id = "MinePlayer")]NetworkCarFacade.Factory playerChaserFactory) : base(signalBus)
        {
            _playerChaserFactory = playerChaserFactory;
        }

        protected override void OnCarInstantiated(CarInstantiateSignal carInstantiateSignal)
        {
            if(PhotonNetwork.LocalPlayer.ActorNumber == carInstantiateSignal.OwnerActor)
                SetViewId(_playerChaserFactory.Create().Transform, carInstantiateSignal);
        }
    }
}