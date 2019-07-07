using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class ChaserDeathHandler: CarDeathHandler
    {
        private readonly ChaserFacade _facade;
        public ChaserDeathHandler(CarView view, AudioPlayer audioPlayer, Explosion.Factory explosionFactory, SignalBus signalBus, ChaserFacade facade) : base(view, audioPlayer, explosionFactory, signalBus)
        {
            _facade = facade;
        }
        
        public override void Die()
        {
            base.Die();
            _facade.Dispose();
        }
    }

    public class AIChaserDeathHandler : ChaserDeathHandler
    {
        public AIChaserDeathHandler(CarView view, AudioPlayer audioPlayer, Explosion.Factory explosionFactory, SignalBus signalBus, ChaserFacade facade) : base(view, audioPlayer, explosionFactory, signalBus, facade)
        {
        }

        public override void Die()
        {
            base.Die();
            SignalBus.Fire<AIChaserKilledSignal>();
        }
    }
    public class PlayerChaserDeathHandler : ChaserDeathHandler
    {
        public PlayerChaserDeathHandler(CarView view, AudioPlayer audioPlayer, Explosion.Factory explosionFactory, SignalBus signalBus, ChaserFacade facade) : base(view, audioPlayer, explosionFactory, signalBus, facade)
        {
        }

        public override void Die()
        {
            base.Die();
            SignalBus.Fire<PlayerChaserKilledSignal>();
        }
    }
}
