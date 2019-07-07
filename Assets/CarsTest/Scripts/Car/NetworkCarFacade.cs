using System;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class NetworkCarFacade: CarFacade
    {
        [Inject] public override void Construct(CarView view, CarDeathHandler deathHandler)
        {
            base.Construct(view, deathHandler);
        }
        
        public class Factory: PlaceholderFactory<NetworkCarFacade>
        {
            
        }
    }
}