using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class CheckpointsModePanel: MonoBehaviour
    {
        [SerializeField] private Text checkpointsCount;

        private CheckpointsModeBehaviour _modeBehaviour;
        private SignalBus _signalBus;

        [Inject]
        public void Construct([InjectOptional]CheckpointsModeBehaviour modeBehaviour, SignalBus signalBus)
        {
            _modeBehaviour = modeBehaviour;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<RunnerTouchCheckpointSignal>(OnRunnerTouchCheckpoint);
            checkpointsCount.text = $"{_modeBehaviour.CurrentCheckpoint}/{_modeBehaviour.CheckpointsCount}";
        }

        private void OnRunnerTouchCheckpoint()
        {
            checkpointsCount.text = $"{_modeBehaviour.CurrentCheckpoint+1}/{_modeBehaviour.CheckpointsCount}";
        }
        
        public class Factory: PlaceholderFactory<CheckpointsModePanel>
        {
            
        }
    }
}