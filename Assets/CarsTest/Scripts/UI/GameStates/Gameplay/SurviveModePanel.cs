using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class SurviveModePanel: MonoBehaviour
    {
        [SerializeField] private Text surviveTimer;

        private SurviveModeBehaviour _modeBehaviour;
        private SignalBus _signalBus;

        [Inject]
        public void Construct([InjectOptional]SurviveModeBehaviour modeBehaviour, SignalBus signalBus)
        {
            _modeBehaviour = modeBehaviour;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            var time = TimeSpan.FromSeconds(_modeBehaviour.GetSecondsToEnd());
            surviveTimer.text = $"{time.Minutes}:{time.Seconds}";
        }

        private void Update()
        {
            var time = TimeSpan.FromSeconds(_modeBehaviour.GetSecondsToEnd());
            surviveTimer.text = $"{time.Minutes}:{time.Seconds}";
        }
        
        public class Factory: PlaceholderFactory<SurviveModePanel>
        {
            
        }
    }
}