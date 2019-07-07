using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class PlayerChaserSpawner : ChaserSpawner
    {
        private int _aliveCars;

        private int NeedCars
        {
            get => _needCars;
            set
            {
                _needCars = value;
                if (_needCars < 0)
                    _needCars = 0;
            }
        }
        private int _needCars;

        public PlayerChaserSpawner([Inject(Id = "PlayerChaser")]ChaserFacade.Factory chaserFactory, SignalBus signalBus, RunnerFacade runnerFacade, LevelInfo levelInfo) : base(chaserFactory, signalBus, runnerFacade, levelInfo)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            SignalBus.Subscribe<PlayerChaserKilledSignal>(OnChaserKilled);
        }
        private void OnChaserKilled()
        {
            _aliveCars--;
        }
        
        protected override void CheckOnSpawn()
        {
            base.CheckOnSpawn();
            if(_aliveCars < NeedCars)
                SpawnEnemy();
        }

        public void Add()
        {
            NeedCars++;
        }
        public void Remove()
        {
            NeedCars--;
        }

        protected override void SpawnEnemy()
        {
            base.SpawnEnemy();
            _aliveCars++;
            
        }
    }
}