using Zenject;

namespace CarsTest
{
    public class RunnerCheckpointHandler
    {
        private readonly SignalBus _signalBus;

        public RunnerCheckpointHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void GotCheckpoint()
        {
            _signalBus.Fire<RunnerTouchCheckpointSignal>();
        }
    }
}