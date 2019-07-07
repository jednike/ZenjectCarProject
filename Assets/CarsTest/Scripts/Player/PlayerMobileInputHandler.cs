using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Zenject;

namespace CarsTest
{
    public class PlayerMobileInputHandler : PlayerInputHandler
    {
        private readonly Settings _settings;
        private readonly CarInput _carInput;

        public PlayerMobileInputHandler(CarInput carInput, Settings settings)
        {
            _carInput = carInput;
            _settings = settings;
        }

        public void Tick()
        {
            _carInput.ThrottleInput = LeftInput && RightInput ? -0.5f : 1f;
            _carInput.SteeringInput = LeftInput && RightInput ? 0f : LeftInput ? -1f : RightInput ? 1f : 0f;
            _carInput.Respawn = RespawnInput;
        }
        
        private bool LeftInput => CrossPlatformInputManager.GetButton(_settings.LeftKey);
        private bool RightInput => CrossPlatformInputManager.GetButton(_settings.RightKey);
        private bool RespawnInput => CrossPlatformInputManager.GetButton(_settings.RespawnKey);
        
        [Serializable]
        public class Settings
        {
            public string LeftKey = "Left";
            public string RightKey = "Right";
            public string RespawnKey = "Respawn";
        }
    }
}
