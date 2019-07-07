using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class ChaserDistanceHandler: ILateTickable
    {
        private readonly RunnerFacade _runnerFacade;
        private readonly ChaserFacade _chaserFacade;
        
        public ChaserDistanceHandler(RunnerFacade runnerFacade, ChaserFacade chaserFacade)
        {
            _runnerFacade = runnerFacade;
            _chaserFacade = chaserFacade;
        }
        
        public void LateTick()
        {
            var distance = Vector3.Distance(_runnerFacade.Position, _chaserFacade.Position);
            if(distance > 100f)
                _chaserFacade.Die();
        }
    }
}