using Zenject;

namespace CarsTest
{
    public class RespawnController: IInitializable, ITickable
    {
        private readonly CarInput _carInput;
        private readonly LevelInfo _levelInfo;
        private readonly CarView _carView;

        public RespawnController(CarInput carInput, LevelInfo levelInfo, CarView carView)
        {
            _carInput = carInput;
            _levelInfo = levelInfo;
            _carView = carView;
        }

        public void Initialize()
        {
            SetDefaultPosition();
        }

        public void Tick()
        {
            if (!_carInput.Respawn) return;
            SetDefaultPosition();
        }

        private void SetDefaultPosition()
        {
            _carView.Position = _levelInfo.runnerPosition.position;
            _carView.Rotation = _levelInfo.runnerPosition.rotation;
        }
    }
}