using Photon.Pun;
using Zenject;

namespace CarsTest.Photon
{
    public class InputFactory: IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly PhotonView _photonView;

        public InputFactory(SignalBus signalBus, [Inject(Id = "Input")] PhotonView photonView)
        {
            _signalBus = signalBus;
            _photonView = photonView;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<InputInstantiateSignal>(OnInputInstantiated);
        }

        private void OnInputInstantiated(InputInstantiateSignal inputInstantiateSignal)
        {
            _photonView.ViewID = inputInstantiateSignal.PhotonId;
        }
    }
}