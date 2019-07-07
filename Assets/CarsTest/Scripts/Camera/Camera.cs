using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private float followSpeed;
        [SerializeField] private float rotationFollowSpeed;

        public CarView Target
        {
            set => target = value;
        }
        [SerializeField] private CarView target;
        
        private void FixedUpdate()
        {
            if (!target) return;

            transform.position = Vector3.Lerp(transform.position, target.Position, Time.fixedDeltaTime * followSpeed);

            var targetForward = target.CarTransform.forward;
            var rollRotation = Quaternion.LookRotation(targetForward, Vector3.up);

            transform.rotation =
                Quaternion.Lerp(transform.rotation, rollRotation, rotationFollowSpeed * Time.fixedDeltaTime);
        }
    }
}