using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CarImmortalPeriod: IInitializable, ITickable
    {
        private float _currentTime;

        private readonly Settings _settings;
        private readonly SignalBus _signalBus;

        public CarImmortalPeriod(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RunnerDiedSignal>(() =>
            {
                _signalBus.Fire(new CarImmortalSignal(true));
                _currentTime = _settings.immortalTime;
            });
        }

        public void Tick()
        {
            if (_currentTime <= 0) return;
            
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                _signalBus.Fire(new CarImmortalSignal(true));
            }
        }
        
        [Serializable]
        public class Settings
        {
            public float immortalTime;
        }
    }
}