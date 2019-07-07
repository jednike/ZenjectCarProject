using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarsTest
{
    [CreateAssetMenu(fileName = "CarsInfo", menuName = "CarsTest/Create Cars Info", order = 1)]
    public class CarsInfo : ScriptableObject
    {
        public CarInfo[] CarInfos;
    }
}