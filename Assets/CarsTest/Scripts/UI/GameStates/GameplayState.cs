using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class GameplayState : GameStateEntity
    {
        [SerializeField] private Transform heartsTransform;

        private GameInfo _gameInfo;
        
        private SignalBus _signalBus;
        
        private Heart.Factory _heartsFactory;
        private DefaultModeBehaviour _modeBehaviour;
        private SurviveModePanel.Factory _surviveModeFactory;
        private CheckpointsModePanel.Factory _checkpointsModeFactory;
        private DestroyModePanel.Factory _destroyModeFactory;
        private MobileControlPanel.Factory _mobileFactory;
        
        private readonly List<Heart> _hearts = new List<Heart>();

        [Inject]
        public void Constructor(GameInfo gameInfo, SignalBus signalBus, Heart.Factory heartsFactory, DefaultModeBehaviour modeBehaviour, SurviveModePanel.Factory surviveModeFactory, CheckpointsModePanel.Factory checkpointsModeFactory, DestroyModePanel.Factory destroyModeFactory, MobileControlPanel.Factory mobileFactory)
        {
            _gameInfo = gameInfo;
            _signalBus = signalBus;
            _heartsFactory = heartsFactory;
            _modeBehaviour = modeBehaviour;
            _surviveModeFactory = surviveModeFactory;
            _checkpointsModeFactory = checkpointsModeFactory;
            _destroyModeFactory = destroyModeFactory;
            _mobileFactory = mobileFactory;
        }

        private void OnRunnerDied()
        {
            if(_hearts.Count == 0)
                return;

            if (_modeBehaviour.RunnerLives < _hearts.Count)
            {
                Destroy(_hearts.Last().gameObject);
                _hearts.Remove(_hearts.Last());
            }
            else if (_modeBehaviour.RunnerLives > _hearts.Count)
            {
                var heart = _heartsFactory.Create();
                heart.transform.SetParent(heartsTransform);
                _hearts.Add(heart);
            }
        }
        
        public new void Start()
        {
            _signalBus.Subscribe<RunnerDiedSignal>(OnRunnerDied);
            for (var i = 0; i < _modeBehaviour.RunnerLives; i++)
            {
                var heart = _heartsFactory.Create();
                heart.transform.SetParent(heartsTransform);
                _hearts.Add(heart);
            }

            switch (_gameInfo.currentGameMode)
            {
                case GameMode.Checkpoint:
                    _checkpointsModeFactory.Create();
                    break;
                case GameMode.Survive:
                    _surviveModeFactory.Create();
                    break;
                case GameMode.Destroy:
                    _destroyModeFactory.Create();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_gameInfo.mobileUi)
                _mobileFactory.Create();
        }
    }
}