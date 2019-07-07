using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class RunnerCollisionHandler: MonoBehaviour
    {
        private RunnerFacade _runnerFacade;
        [Inject]
        public void Construct(RunnerFacade runnerFacade)
        {
            _runnerFacade = runnerFacade;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Road") && other.relativeVelocity.magnitude > 20f)
            {
                _runnerFacade.Die();
            }
        }
        private void OnTriggerEnter(Collider colider)
        {
            if(colider.CompareTag("Checkpoint"))
                _runnerFacade.GotCheckpoint();
        }
    }
}