namespace CarsTest
{
    public class GameModeEndSignal
    {
        public GameModeEndSignal(bool success)
        {
            Success = success;
        }
        
        public bool Success { get; }
    }
    public class GameStateChangedSignal
    {
        public GameStateChangedSignal(GameState lastState, GameState newState)
        {
            LastState = lastState;
            NewState = newState;
        }

        public GameState LastState { get; private set; }
        public GameState NewState { get; private set; }
    }
    public struct RunnerTouchCheckpointSignal
    {
    }
    public struct RunnerDiedSignal
    {
    }

    public struct AIChaserKilledSignal
    {
    }
    public struct PlayerChaserKilledSignal
    {
    }
    public struct CarImmortalSignal
    {
        public CarImmortalSignal(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
    }
}
