using UnityEngine;

namespace CarsTest
{
    public class AIInfo: MonoBehaviour
    {
        public float minDisance = 3f;
	
        public float frontDistanceReaction = 50f;
        public float backDistanceReaction = 30f;
        public float sideDistanceReaction = 15f;
	
        public Transform sensorPosition;
        public float frontOffset = 1f;
        public float backOffset = 1f;
        public float sideOffset = .5f;
    }
}