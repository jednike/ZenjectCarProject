using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Settings settings;
        
        public override void InstallBindings()
        {
            Container.BindFactory<Heart, Heart.Factory>()
                .FromComponentInNewPrefab(settings.heartPrefab)
                .UnderTransform(transform);
            
            Container.BindFactory<DestroyModePanel, DestroyModePanel.Factory>()
                .FromComponentInNewPrefab(settings.destroyPanelPrefab)
                .UnderTransform(transform);
            Container.BindFactory<CheckpointsModePanel, CheckpointsModePanel.Factory>()
                .FromComponentInNewPrefab(settings.checkpointsModePanelPrefab)
                .UnderTransform(transform);
            Container.BindFactory<SurviveModePanel, SurviveModePanel.Factory>()
                .FromComponentInNewPrefab(settings.surviveModePanelPrefab)
                .UnderTransform(transform);
            
            Container.BindFactory<MobileControlPanel, MobileControlPanel.Factory>()
                .FromComponentInNewPrefab(settings.mobileControlPanelPrefab)
                .UnderTransform(transform);
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject heartPrefab;
            
            public GameObject checkpointsModePanelPrefab;
            public GameObject surviveModePanelPrefab;
            public GameObject destroyPanelPrefab;
            
            public GameObject mobileControlPanelPrefab;
        }
    }
}