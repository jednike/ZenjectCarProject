using System;
using UnityEngine;

namespace CarsTest
{
    public enum PlayerType
    {
        Runner,
        Chaser
    }
    public enum GameType
    {
        Offline,
        Online
    }
    [Serializable]
    public class GameInfo
    {
        public PlayerType playerType;
        public GameMode currentGameMode;
        public GameType gameType;
        
        public bool mobileUi;

        public bool win;

        public int selectedLevel;
        public int selectedCar;
    }
}