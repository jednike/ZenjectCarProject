using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarsTest
{
    [CreateAssetMenu(fileName = "LevelsInfo", menuName = "CarsTest/Create Levels Info", order = 1)]
    public class ScenesInfo : ScriptableObject
    {
        public SceneInfo[] LevelInfos;
    }
}
