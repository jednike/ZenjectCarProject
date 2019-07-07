using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public enum GameState
    {
        None,
        Menu,
        SelectCar,
        SelectLevel,
        Pause,
        GamePlay,
        EndGame,
        SelectType,
        Loading,
        Dialog
    }

    public class GameStateManager : ILateTickable, ITickable, IInitializable
    {
        private GameStateEntity _gameStateEntity;

        public GameState CurrentGameState => _currentGameState;
        private GameState _currentGameState;
        private readonly List<GameState> _lastStates = new List<GameState>();

        private readonly SignalBus _signalBus;

        public GameStateManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void ChangeState(GameState gameState)
        {
            if (_gameStateEntity != null)
            {
                _gameStateEntity.Dispose();
                _gameStateEntity = null;
            }

            var lastState = _currentGameState;
            _currentGameState = gameState;

            if (lastState == gameState)
                return;

            var needPop = false;
            switch (lastState)
            {
                case GameState.Pause:
                    if (gameState == GameState.Menu || gameState == GameState.GamePlay)
                        needPop = true;
                    break;
                case GameState.GamePlay:
                    if (gameState != GameState.Pause || gameState != GameState.Menu)
                        needPop = true;
                    break;
                case GameState.EndGame:
                    if (_lastStates.Last() != GameState.Menu)
                        needPop = true;
                    break;
                default:
                    needPop = false;
                    break;
            }

            if (needPop)
            {
                if (_lastStates.Count > 1)
                    _lastStates.Remove(_lastStates.Last());
            }
            else
                _lastStates.Add(lastState);

            _signalBus.Fire<GameStateChangedSignal>(new GameStateChangedSignal(lastState, gameState));

            if (_gameStateEntity != null)
            {
                _gameStateEntity.Start();
            }
        }

        public void ToLastState()
        {
            switch (_currentGameState)
            {
                case GameState.GamePlay:
                    ChangeState(GameState.Pause);
                    break;
                case GameState.Pause:
                    ChangeState(GameState.GamePlay);
                    break;
                case GameState.Menu:
                case GameState.Loading:
                case GameState.Dialog:
                    break;
                case GameState.SelectCar:
                    ChangeState(GameState.Menu);
                    break;
                case GameState.SelectLevel:
                    ChangeState(GameState.SelectType);
                    break;
                case GameState.SelectType:
                    ChangeState(GameState.Menu);
                    break;
                default:
                    ChangeState(_lastStates.LastOrDefault() == GameState.None
                        ? GameState.Menu
                        : _lastStates.LastOrDefault());
                    break;
            }
        }

        public void LateTick()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                ToLastState();
        }

        public void Initialize()
        {
        }

        public void Tick()
        {
            _gameStateEntity?.Update();
        }
    }
}