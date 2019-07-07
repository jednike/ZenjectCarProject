using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class SelectLevelState : GameStateEntity
    {
        [SerializeField] private Transform levelsTransform;
        [SerializeField] private Button nextButton;
    
        private LevelCard _currentCard;
        private ScenesInfo _levelsInfo;
        private LevelCard.Factory _factory;
        private NetworkConnector _networkConnector;

        [Inject]
        public void Constructor(GameStateManager gameStateManager, GameInfo gameInfo, ScenesInfo levelsInfo, LevelCard.Factory factory, NetworkConnector networkConnector)
        {
            base.Constructor(gameStateManager, gameInfo);
            _levelsInfo = levelsInfo;
            _factory = factory;
            _networkConnector = networkConnector;
        }
        
        private void Awake()
        {
            var i = 0;
            foreach (var level in _levelsInfo.LevelInfos)
            {
                var levelCard = _factory.Create();
                levelCard.transform.SetParent(levelsTransform);
                levelCard.SetInfo(level, i, ActionOnClick);

                levelCard.Select(false);
                if (!_currentCard || i == Scores.Instance.SelectedLevel)
                    _currentCard = levelCard;
                i++;
            }
            _currentCard.Select(true);
            
            if(GameInfo.gameType == GameType.Online)
                nextButton.onClick.AddListener(StartOnlineGame);
            else
                nextButton.onClick.AddListener(StartOfflineGame);
        }

        private void StartOfflineGame()
        {
            GameStateManager.ChangeState(GameState.Loading);
        }
        private void StartOnlineGame()
        {
            _networkConnector.ConnectNow(PlayerType.Runner);
        }

        private void ActionOnClick(LevelCard card)
        {
            if(_currentCard == card)
                return;
            GameInfo.selectedLevel = card.Index;
            _currentCard.Select(false);
            _currentCard = card;
            _currentCard.Select(true);
            Scores.Save();
        }
    }
}