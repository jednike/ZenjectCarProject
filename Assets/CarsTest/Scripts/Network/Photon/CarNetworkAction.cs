using Photon.Pun;
using Zenject;

namespace CarsTest.Photon
{
    public class CarNetworkAction: IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly PhotonView _photonView;

        public CarNetworkAction(SignalBus signalBus, [Inject(Id = "Car")] PhotonView photonView)
        {
            _signalBus = signalBus;
            _photonView = photonView;
        }

        public void Initialize()
        {
        }

        private void DestroyCar()
        {
            _photonView.RPC("DestroyCar", RpcTarget.Others);
        }
    }
}