using System;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class CarInstantiator: IInitializable
    {
        private readonly CarView _view;
        private readonly PhotonView _photonView;
        private readonly PlayerType _playerType;
        private readonly bool _ai;
        private readonly int _ownerActor;

        public CarInstantiator(CarView view, [Inject(Id = "Car")] PhotonView photonView, PlayerType playerType, bool ai, int ownerActor)
        {
            _view = view;
            _photonView = photonView;
            _playerType = playerType;
            _ai = ai;
            _ownerActor = ownerActor;
        }

        public void Initialize()
        {
            if (!PhotonNetwork.AllocateViewID(_photonView))
                return;

            object[] data =
            {
                _view.Position, _view.Rotation, _photonView.ViewID, _playerType, _ai, _ownerActor
            };

            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            var raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = EventCaching.AddToRoomCache
            };

            raiseEventOptions.Receivers = ReceiverGroup.Others;
            PhotonNetwork.RaiseEvent(Constants.CarInstantiateEventCode, data, raiseEventOptions, sendOptions);
        }
    }
}