using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class AIChaserSpawner : ChaserSpawner
    {
        private readonly Settings _settings;

        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;

        public AIChaserSpawner(
            [Inject(Id = "AIChaser")]ChaserFacade.Factory chaserFactory,
            SignalBus signalBus,
            LevelInfo levelInfo, RunnerFacade runnerFacade,
            Settings settings) : base(chaserFactory, signalBus, runnerFacade, levelInfo)
        {
            _settings = settings;
            _desiredNumEnemies = settings.numEnemiesStartAmount;
        }

        public override void Initialize()
        {
            base.Initialize();
            SignalBus.Subscribe<AIChaserKilledSignal>(OnChaserKilled);
        }
        private void OnChaserKilled()
        {
            _enemyCount--;
        }
        
        protected override void CheckOnSpawn()
        {
            base.CheckOnSpawn();
            if(!_settings.fixedCount)
                _desiredNumEnemies += _settings.numEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount >= (int) _desiredNumEnemies || Time.realtimeSinceStartup - _lastSpawnTime <= _settings.minDelayBetweenSpawns) return;
            SpawnEnemy();
        }

        protected override void SpawnEnemy()
        {
            base.SpawnEnemy();
            _lastSpawnTime = Time.realtimeSinceStartup;
            _enemyCount++;
        }
        
        [Serializable]
        public class Settings
        {
            [Tooltip("Need add enemies by time?")] public bool fixedCount;
            
            [Tooltip("Added enemies per second")] public float numEnemiesIncreaseRate;
            [Tooltip("How many enemies on start?")] public float numEnemiesStartAmount;

            [Tooltip("Min time between enemy spawn")] public float minDelayBetweenSpawns = 0.5f;
        }
    }
}