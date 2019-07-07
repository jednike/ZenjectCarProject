using UnityEngine;

namespace CarsTest
{
    public class TrashRigids : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_rigidbody.isKinematic) return;
            if (!(other.relativeVelocity.magnitude > 10f)) return;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForceAtPosition(other.relativeVelocity * 10f, other.GetContact(0).point, ForceMode.Acceleration);
        }
    }
}