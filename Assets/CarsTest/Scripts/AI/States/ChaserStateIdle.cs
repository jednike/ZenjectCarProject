using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CarsTest
{
    public class ChaserStateIdle : IChaserState
    {
        private readonly CarView _view;
        private readonly CarInput _input;

        public ChaserStateIdle(CarView view, CarInput input)
        {
            _view = view;
            _input = input;
        }

        public void EnterState()
        {
            _input.SteeringInput = 0;
            _input.ThrottleInput = 0;
        }

        public void ExitState()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

    }
}
