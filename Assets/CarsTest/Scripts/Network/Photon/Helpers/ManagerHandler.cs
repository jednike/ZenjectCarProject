using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class ManagerHandler: IInitializable, IDisposable, IOnEventCallback
    {
        private readonly SignalBus _signalBus;
        public ManagerHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case Constants.ManagerInstantiateEventCode:
                    var data = (object[]) photonEvent.CustomData;
                    var viewId = (int) data[0];
                    _signalBus.Fire(new ManagerInstantiateSignal(viewId));
                    break;
                case Constants.ManagerRunnerWinEventCode:
                    _signalBus.Fire(new GameModeEndSignal(true));
                    break;
                case Constants.ManagerRunnerLoseEventCode:
                    _signalBus.Fire(new GameModeEndSignal(false));
                    break;
            }
        }

        public void Dispose()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}