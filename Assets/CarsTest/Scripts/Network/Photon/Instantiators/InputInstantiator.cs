using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CarsTest.Photon
{
    public class InputInstantiator: IInitializable
    {
        private readonly PhotonView _photonView;
        public InputInstantiator([Inject(Id = "Input")] PhotonView photonView)
        {
            _photonView = photonView;
        }
        
        public void Initialize()
        {
            if (!PhotonNetwork.AllocateViewID(_photonView))
                return;
            
            object[] data = {
                _photonView.ViewID
            };
            var raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };
            var sendOptions = new SendOptions
            {
                Reliability = true
            };
            PhotonNetwork.RaiseEvent(Constants.InputInstantiateEventCode, data, raiseEventOptions, sendOptions);
        }
    }
}