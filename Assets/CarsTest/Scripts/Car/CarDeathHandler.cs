using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CarDeathHandler
    {
        protected readonly SignalBus SignalBus;
        
        private readonly Explosion.Factory _explosionFactory;
        private readonly AudioPlayer _audioPlayer;
        private readonly CarView _view;

        public CarDeathHandler(
            CarView view,
            AudioPlayer audioPlayer,
            Explosion.Factory explosionFactory,
            SignalBus signalBus)
        {
            SignalBus = signalBus;
            _explosionFactory = explosionFactory;
            _audioPlayer = audioPlayer;
            _view = view;
        }

        public virtual void Die()
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _view.Position;
        }
    }
}
