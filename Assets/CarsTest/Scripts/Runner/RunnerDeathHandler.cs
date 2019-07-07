using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class RunnerDeathHandler: CarDeathHandler
    {
        public RunnerDeathHandler(CarView view, AudioPlayer audioPlayer, Explosion.Factory explosionFactory, SignalBus signalBus) : base(view, audioPlayer, explosionFactory, signalBus)
        {
        }
        
        public override void Die()
        {
            SignalBus.Fire<RunnerDiedSignal>();
        }
    }
}
