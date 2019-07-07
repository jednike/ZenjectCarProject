using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class RunnerFacade: MonoBehaviour, ICarFacade
    {
        private CarView _view;
        private RunnerDeathHandler _deathHandler;
        private RunnerCheckpointHandler _checkpointHandler;
        
        [Inject]
        public void Construct(CarView view, RunnerDeathHandler deathHandler, RunnerCheckpointHandler checkpointHandler)
        {
            _view = view;
            _deathHandler = deathHandler;
            _checkpointHandler = checkpointHandler;

            _view.CarTransform.tag = "Runner";
        }
        
        public void Die()
        {
            _deathHandler.Die();
        }
        public void GotCheckpoint()
        {
            _checkpointHandler.GotCheckpoint();
        }

        public Transform Transform => _view.CarTransform;
        public Vector3 Position => _view.Position;
        
        public class Factory : PlaceholderFactory<RunnerFacade>
        {
        }
    }
}