using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class PhotonInputInstantiator: IInitializable
    {
        private readonly PhotonView _photonView;
        private readonly NetworkInput _networkInput;

        public PhotonInputInstantiator([Inject(Id = "Input")] PhotonView photonView, NetworkInput networkInput)
        {
            _photonView = photonView;
            _networkInput = networkInput;
        }

        public void Initialize()
        {
            _photonView.Synchronization = ViewSynchronization.UnreliableOnChange;
            _photonView.ObservedComponents = new List<Component>{_networkInput};
        }
    }
}