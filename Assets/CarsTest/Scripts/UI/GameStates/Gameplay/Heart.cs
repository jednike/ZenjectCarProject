using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class Heart: MonoBehaviour
    {
        public class Factory: PlaceholderFactory<Heart>
        {
            
        }
    }
}