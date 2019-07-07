using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class DestroyModePanel: MonoBehaviour
    {
        [SerializeField] private Text carsCount;

        private DefaultModeBehaviour _modeBehaviour;
        private SignalBus _signalBus;

        [Inject]
        public void Construct([InjectOptional]DefaultModeBehaviour modeBehaviour, SignalBus signalBus)
        {
            _modeBehaviour = modeBehaviour;
            _signalBus = signalBus;
        }

        private void Awake()
        {
        }
        public class Factory: PlaceholderFactory<DestroyModePanel>
        {
            
        }
    }
}