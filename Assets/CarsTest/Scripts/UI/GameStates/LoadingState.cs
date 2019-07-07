using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class LoadingState: GameStateEntity
    {
        private ScenesInfo _levelsInfo;
        [Inject]
        public void Constructor(GameStateManager gameStateManager, GameInfo gameInfo, ScenesInfo levelsInfo)
        {
            base.Constructor(gameStateManager, gameInfo);
            _levelsInfo = levelsInfo;
        }

        public override void Start()
        {
            SceneManager.LoadScene(_levelsInfo.LevelInfos[GameInfo.selectedLevel].Path);
        }
    }
}