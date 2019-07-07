using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CarsTest
{
    public class ChaserSpawner : ITickable, IInitializable
    {
        protected readonly ChaserFacade.Factory ChaserFactory;
        protected readonly SignalBus SignalBus;
        private readonly RunnerFacade _runnerFacade;
        private readonly LevelInfo LevelInfo;

        [Inject] private readonly GameStateManager _gameStateManager;

        public ChaserSpawner(ChaserFacade.Factory chaserFactory, SignalBus signalBus, RunnerFacade runnerFacade, LevelInfo levelInfo)
        {
            ChaserFactory = chaserFactory;
            SignalBus = signalBus;
            _runnerFacade = runnerFacade;
            LevelInfo = levelInfo;
        }

        public void Tick()
        {
            switch (_gameStateManager.CurrentGameState)
            {
                case GameState.GamePlay:
                    CheckOnSpawn();
                    break;
            }
        }

        protected virtual void CheckOnSpawn()
        {
            
        }

        public virtual void Initialize()
        {
        }

        protected virtual void SpawnEnemy()
        {
            var chaserFacade = ChaserFactory.Create();
            if (ChooseRandomStartPosition(out var position, out var rotation))
            {
                chaserFacade.Position = position;
                chaserFacade.Rotation = rotation;
            }
            else
            {
                chaserFacade.Position = -_runnerFacade.Transform.forward*20;
                chaserFacade.Rotation = _runnerFacade.Transform.rotation;
            }
        }
        
        protected bool ChooseRandomStartPosition(out Vector3 position, out Quaternion rotation)
        {
            var tries = 0;
            const float distance = 35f;
            const int maxTries = 10;
            bool down;
            bool obstacle;
            
            position = Vector3.zero;
            rotation = Quaternion.identity;
            do
            {
                var circle = Random.insideUnitCircle * distance;
                position = _runnerFacade.Position + new Vector3(circle.x, _runnerFacade.Position.y, circle.y);
                rotation = Quaternion.LookRotation(_runnerFacade.Position - position);
                tries++;
                down = Physics.Raycast(position, Vector3.down);
                obstacle = Physics.Linecast(_runnerFacade.Position, position);
            } while ((obstacle || !down) && tries < maxTries);
            
            return !obstacle && down;
        }
    }
}