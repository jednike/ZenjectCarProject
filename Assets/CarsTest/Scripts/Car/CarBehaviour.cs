using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CarBehaviour : ILateTickable, IFixedTickable
    {
        private readonly CarPhysics _carPhysics;
        public CarBehaviour(CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
        }

        public void LateTick()
        {
            _carPhysics.LateTick();
        }

        public void FixedTick()
        {
            _carPhysics.FixedTick();
        }
    }
}