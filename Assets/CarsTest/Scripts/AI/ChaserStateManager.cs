using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public interface IChaserState
    {
        void EnterState();
        void ExitState();
        void Update();
        void FixedUpdate();
    }

    public enum ChaserStates
    {
        Idle,
        Follow,
        None
    }

    public class ChaserStateManager : ITickable, IFixedTickable, IInitializable
    {
        private IChaserState _currentStateHandler;
        private CarView _view;
        private List<IChaserState> _states;

        private GameStateManager _gameStateManager;
        private ChaserStates _currentState = ChaserStates.None;
        public ChaserStates CurrentState => _currentState;

        [Inject]
        public void Construct(
            CarView view,
            ChaserStateIdle idle, ChaserStateFollow follow, GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            _view = view;
            _states = new List<IChaserState>
            {
                idle, follow
            };
        }


        public void Initialize()
        {
            Assert.IsEqual(_currentState, ChaserStates.None);
            Assert.IsNull(_currentStateHandler);

            ChangeState(ChaserStates.Follow);
        }

        public void ChangeState(ChaserStates state)
        {
            if (_currentState == state)
            {
                return;
            }

            _currentState = state;
            if (_currentStateHandler != null)
            {
                _currentStateHandler.ExitState();
                _currentStateHandler = null;
            }

            _currentStateHandler = _states[(int)state];
            _currentStateHandler.EnterState();
        }

        public void Tick()
        {
            if(_gameStateManager.CurrentGameState != GameState.GamePlay)
                return;
            _currentStateHandler.Update();
        }

        public void FixedTick()
        {
            if(_gameStateManager.CurrentGameState != GameState.GamePlay)
                return;
            _currentStateHandler.FixedUpdate();
        }
    }
}
