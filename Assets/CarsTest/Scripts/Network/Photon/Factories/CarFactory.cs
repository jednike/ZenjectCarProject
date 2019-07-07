using System;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class CarFactory: EventCarFactory
    {
        private readonly NetworkCarFacade.Factory _runnerFactory;
        private readonly NetworkCarFacade.Factory _aiChaserFactory;
        private readonly NetworkCarFacade.Factory _playerChaserFactory;
        
        public CarFactory([Inject(Id = "Runner")]NetworkCarFacade.Factory runnerFactory, [Inject(Id = "AI")]NetworkCarFacade.Factory aiChaserFactory, [Inject(Id = "Player")]NetworkCarFacade.Factory playerChaserFactory, SignalBus signalBus): base(signalBus)
        {
            _runnerFactory = runnerFactory;
            _aiChaserFactory = aiChaserFactory;
            _playerChaserFactory = playerChaserFactory;
        }


        protected override void OnCarInstantiated(CarInstantiateSignal carInstantiateSignal)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == carInstantiateSignal.OwnerActor)
                return;
            Transform carTransform;
            if (carInstantiateSignal.CarType == PlayerType.Runner)
                carTransform = _runnerFactory.Create().Transform;
            else
                carTransform = carInstantiateSignal.Ai ? _aiChaserFactory.Create().Transform : _playerChaserFactory.Create().Transform;
            SetViewId(carTransform, carInstantiateSignal);
        }
    }
}