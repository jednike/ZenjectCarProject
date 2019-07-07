using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CarInactivityRespawn: ILateTickable
    {
        private readonly Settings _settings;
        private readonly CarView _view;
        private readonly ICarFacade _facade;
        private readonly GameStateManager _gameStateManager;
        private float _deathTime;

        public CarInactivityRespawn(Settings settings, CarView view, ICarFacade facade, GameStateManager gameStateManager)
        {
            _settings = settings;
            _view = view;
            _facade = facade;
            _gameStateManager = gameStateManager;
        }

        public void LateTick()
        {
            if(_gameStateManager.CurrentGameState != GameState.GamePlay)
                return;
            
            if (_view.Speed < 1f)
            {
                _deathTime += Time.deltaTime;
                if (_deathTime < _settings.inactivityTime) return;
                _facade.Die();
                _deathTime = 0f;
                return;
            }
            _deathTime = 0f;
        }
        
        [Serializable]
        public class Settings
        {
            public float inactivityTime = 4f;
        }
    }
}