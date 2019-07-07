using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class SelectTypeState: GameStateEntity
    {
        [SerializeField] private Button runnerButton;
        [SerializeField] private Button chaserButton;

        [Inject] private NetworkConnector _networkConnector;

        public override void Start()
        {
            GameInfo.gameType = GameType.Online;
            chaserButton.onClick.AddListener(() =>
            {
                _networkConnector.ConnectNow(PlayerType.Chaser);
            });
        }
    }
}