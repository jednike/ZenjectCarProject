using System;
using Zenject;

namespace CarsTest
{
    public class PlayerInputManager: ITickable
    {
        private readonly PlayerInputHandler _inputHandler;
        private readonly GameStateManager _gameStateManager;

        public PlayerInputManager(PlayerInputHandler inputHandler, GameStateManager gameStateManager)
        {
            _inputHandler = inputHandler;
            _gameStateManager = gameStateManager;
        }

        public void Tick()
        {
            switch (_gameStateManager.CurrentGameState)
            {
                case GameState.GamePlay:
                    _inputHandler.Tick();
                    break;
                default:
                    break;
            }
        }
    }
}