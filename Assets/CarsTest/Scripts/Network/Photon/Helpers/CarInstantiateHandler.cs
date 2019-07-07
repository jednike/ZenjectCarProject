using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class CarInstantiateHandler: IInitializable, IDisposable, IOnEventCallback
    {
        private readonly SignalBus _signalBus;
        public CarInstantiateHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code != Constants.CarInstantiateEventCode) return;
            
            var data = (object[]) photonEvent.CustomData;
            var position = (Vector3) data[0];
            var rotation = (Quaternion) data[1];
            var viewId = (int) data[2];
            var carType = (PlayerType) data[3];
            var ai = (bool) data[4];
            var ownerActor = (int) data[5];

            _signalBus.Fire(new CarInstantiateSignal(position, rotation, viewId, carType, ai, ownerActor));
        }

        public void Dispose()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}