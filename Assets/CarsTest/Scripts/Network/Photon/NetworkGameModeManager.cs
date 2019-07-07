using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CarsTest.Photon
{
    public class NetworkGameModeManager: IInitializable
    {
        private readonly SignalBus _signalBus;
        
        public NetworkGameModeManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public virtual void Initialize()
        {
            _signalBus.Subscribe<GameModeEndSignal>(OnGameModeEnd);
        }

        private void OnGameModeEnd(GameModeEndSignal gameModeEndSignal)
        {
            var raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };
            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent(gameModeEndSignal.Success ? Constants.ManagerRunnerWinEventCode: Constants.ManagerRunnerLoseEventCode, null, raiseEventOptions, sendOptions);
        }
    }
}