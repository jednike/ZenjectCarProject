using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class GameModeObserver: IInitializable
    {
        private readonly PhotonView _photonView;
        private readonly NetworkGameModeObserver _gameModeObserver;

        public GameModeObserver(PhotonView photonView, NetworkGameModeObserver gameModeObserver)
        {
            _photonView = photonView;
            _gameModeObserver = gameModeObserver;
        }

        public void Initialize()
        {
            _photonView.Synchronization = ViewSynchronization.UnreliableOnChange;
            _photonView.ObservedComponents = new List<Component>{_gameModeObserver};
        }
    }

    public class NetworkGameModeObserver: MonoBehaviour, IPunObservable
    {
        public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }
    }
}