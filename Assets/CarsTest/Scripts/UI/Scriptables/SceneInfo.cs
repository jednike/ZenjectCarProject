using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarsTest
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "CarsTest/Create Level Info", order = 1)]
    public class SceneInfo : ScriptableObject
    {
        public string Path;
        public string Name;
        public Sprite Image;
    }
}