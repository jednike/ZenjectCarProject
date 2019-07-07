using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CarsTest.Photon
{
    public class PlayerController: MonoBehaviourPunCallbacks
    {
        public static int OwnerId;
        private PlayerChaserSpawner _chaserSpawner;
        [Inject] public void Construct(PlayerChaserSpawner chaserSpawner)
        {
            _chaserSpawner = chaserSpawner;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            OwnerId = newPlayer.ActorNumber;
            _chaserSpawner.Add();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            _chaserSpawner.Remove();
        }
    }
}