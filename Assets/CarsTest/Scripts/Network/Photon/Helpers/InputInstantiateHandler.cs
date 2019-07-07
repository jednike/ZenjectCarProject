using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CarsTest.Photon
{
    public class InputInstantiateHandler: IInitializable, IOnEventCallback, IDisposable
    {
        private readonly SignalBus _signalBus;
        public InputInstantiateHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code != Constants.InputInstantiateEventCode) return;
            
            var data = (object[]) photonEvent.CustomData;
            var viewId = (int) data[0];
            
            _signalBus.Fire(new InputInstantiateSignal(viewId));
        }
        public void Dispose()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}