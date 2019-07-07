using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    [CreateAssetMenu(menuName = "CarsTest/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings gameInstaller;
        public AIChaserSpawner.Settings chaserSpawner;
        public PlayerMobileInputHandler.Settings mobileInputSettings;
        public PlayerStandaloneInputHandler.Settings standaloneInputSettings;
        
        public DefaultModeInfo defaultModeInfo;
        public SurviveModeInfo surviveModeInfo;
        public DestroyModeInfo destroyModeInfo;
        public CheckPointsModeInfo checkpointsModeInfo;
        public CarInactivityRespawn.Settings inactivityRespawnSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gameInstaller).IfNotBound();
            Container.BindInstance(chaserSpawner).IfNotBound();
            Container.BindInstance(mobileInputSettings).IfNotBound();
            Container.BindInstance(standaloneInputSettings).IfNotBound();
            
            Container.BindInstance(defaultModeInfo).IfNotBound();
            Container.BindInstance(surviveModeInfo).IfNotBound();
            Container.BindInstance(destroyModeInfo).IfNotBound();
            Container.BindInstance(checkpointsModeInfo).IfNotBound();
            Container.BindInstance(inactivityRespawnSettings).IfNotBound();
        }
    }
}

