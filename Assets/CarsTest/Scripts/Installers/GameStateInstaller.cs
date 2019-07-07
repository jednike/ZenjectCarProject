using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class GameStateInstaller: MonoInstaller
    {
        [Inject] private readonly Settings _settings = null;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
            
            Container.BindFactory<CarCard, CarCard.Factory>()
                .FromComponentInNewPrefab(_settings.carCard);
            Container.BindFactory<LevelCard, LevelCard.Factory>()
                .FromComponentInNewPrefab(_settings.levelCard);
            
            BindSignals();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject carCard;
            public GameObject levelCard;
        }
    }
}