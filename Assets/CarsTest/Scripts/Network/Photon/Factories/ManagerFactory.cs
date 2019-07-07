using Photon.Pun;
using Zenject;

namespace CarsTest.Photon
{
    public class ManagerFactory: IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly PhotonView _photonView;

        public ManagerFactory(SignalBus signalBus, PhotonView photonView)
        {
            _signalBus = signalBus;
            _photonView = photonView;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ManagerInstantiateSignal>(OnManagerInstantiated);
        }

        private void OnManagerInstantiated(ManagerInstantiateSignal managerInstantiateSignal)
        {
            _photonView.ViewID = managerInstantiateSignal.PhotonId;
        }
    }
}