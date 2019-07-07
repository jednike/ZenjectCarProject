using System.Collections.Generic;
using Photon.Pun;
using Smooth;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class PhotonCarInstantiator: IInitializable
    {
        private readonly PhotonView _photonView;
        private readonly SmoothSyncPUN2 _smoothSync;

        public PhotonCarInstantiator([Inject(Id = "Car")] PhotonView photonView, SmoothSyncPUN2 smoothSync)
        {
            _photonView = photonView;
            _smoothSync = smoothSync;
        }

        public void Initialize()
        {
            _photonView.Synchronization = ViewSynchronization.UnreliableOnChange;
            _photonView.ObservedComponents = new List<Component>{_smoothSync};
        }
    }
}