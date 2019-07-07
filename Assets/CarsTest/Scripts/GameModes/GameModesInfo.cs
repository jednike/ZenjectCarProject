using System;

namespace CarsTest
{
    public enum GameMode
    {
        Checkpoint,
        Survive,
        Destroy
    }
    [Serializable]
    public class DestroyModeInfo
    {
        public int ChaserLives = 20;
    }
    [Serializable]
    public class CheckPointsModeInfo
    {
    }
    [Serializable]
    public class SurviveModeInfo
    {
        public float SurviveTime;
    }
    [Serializable]
    public class DefaultModeInfo
    {
        public bool Immortal = false;
        public int RunnerLives = 3;
    }
}