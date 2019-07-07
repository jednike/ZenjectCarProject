using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class PlayerStandaloneInputHandler : PlayerInputHandler
    {
        private readonly Settings _settings;
        private readonly CarInput _carInput;

        public PlayerStandaloneInputHandler(CarInput carInput, Settings settings)
        {
            _carInput = carInput;
            _settings = settings;
        }

        public void Tick()
        {
            _carInput.ThrottleInput = Input.GetAxis(_settings.VerticalKey);
            _carInput.ThrottleInput = Mathf.Clamp(_carInput.ThrottleInput, -0.5f, 1f);
            _carInput.SteeringInput = Input.GetAxis(_settings.HorizontalKey);
        }
        
        [Serializable]
        public class Settings
        {
            public string HorizontalKey = "Horizontal";
            public string VerticalKey = "Vertical";
            public string RespawnKey = "Respawn";
        }
    }
}
