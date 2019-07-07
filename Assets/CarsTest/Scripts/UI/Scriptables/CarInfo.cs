using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarsTest
{
    [CreateAssetMenu(fileName = "CarInfo", menuName = "CarsTest/Create Car Info", order = 1)]
    public class CarInfo : ScriptableObject
    {
        public GameObject prefab;
        public Material[] materials;
        public string Name;
        public Sprite Image;
    }
}