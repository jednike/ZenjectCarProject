using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CarView: MonoBehaviour
    {
        [Serializable]
        public class WheelInfo
        {
            public bool haveMotor;
            public bool mayRotate;
            public Transform wheelTransform;
            public WheelCollider wheelCollider;
        }
        [SerializeField] private Renderer[] bodyRenderers;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Transform centerOfMass;
        [SerializeField] private Transform carTransform;
        [SerializeField] private WheelInfo[] wheels;
        
        [SerializeField] private float downforce = 10f;
        [SerializeField] private float steeringPrecision = 1f;
        [SerializeField] private float throttlePrecision = 1f;
        [SerializeField] private float distancePrecision = 1f;
        [SerializeField] private float distanceToStop = 1f;
        [SerializeField] private float maxForwardVelocity = 20f;
        [SerializeField] private float maxBackwardVelocity = 10f;
        [SerializeField] private float maxAngularVelocity = 5f;

        public Renderer[] BodyRenderers => bodyRenderers;
        public Rigidbody Rigidbody => rigidBody;
        public WheelInfo[] Wheels => wheels;
        public Transform CenterOfMass => centerOfMass;
        public Transform CarTransform => carTransform;

        public Vector3 Position
        {
            get => carTransform.position;
            set => carTransform.position = value;
        }
        public Quaternion Rotation
        {
            get => carTransform.rotation;
            set => carTransform.rotation = value;
        }

        public Vector3 Velocity
        {
            get => rigidBody.velocity;
            set => rigidBody.velocity = value;
        }

        public Vector3 AngularVelocity
        {
            get => rigidBody.angularVelocity;
            set => rigidBody.angularVelocity = value;
        }

        public float Downforce => downforce;
        public float SteeringPrecision => steeringPrecision;
        public float ThrottlePrecision => throttlePrecision;
        public float DistancePrecision => distancePrecision;
        public float DistanceToStop => distanceToStop;
        public float MaxForwardVelocity => maxForwardVelocity;
        public float MaxBackwardVelocity => maxBackwardVelocity;
        public float MaxAngularVelocity => maxAngularVelocity;

        public void AddForce(Vector3 force)
        {
            rigidBody.AddForce(force);
        }
        public void AddTorque(float value)
        {
            rigidBody.AddTorque(Vector3.forward * value);
        }
        public bool IsGrounded
        {
            get
            {
                var isGrounded = false;
                foreach (var wheel in Wheels)
                {
                    if (wheel.haveMotor && wheel.wheelCollider.isGrounded)
                        isGrounded = true;
                }
                return isGrounded;
            }
        }
        
        public float Speed { get; set; }

        public void SetMaterial(Material material)
        {
            foreach (var render in bodyRenderers)
            {
                render.material = material;
            }
        }
    }
}