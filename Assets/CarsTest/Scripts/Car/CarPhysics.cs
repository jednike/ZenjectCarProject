using UnityEngine;

namespace CarsTest
{
    public class CarPhysics
    {
        private readonly CarView _view;
        private readonly CarInput _input;

        public CarPhysics(CarView view, CarInput input)
        {
            _view = view;
            _input = input;
        }
        
        public virtual void LateTick()
        {
            _view.Speed = _view.CarTransform.InverseTransformDirection(_view.Velocity).z * 3.6f;
            foreach (var wheel in _view.Wheels)
            {
                if(wheel.mayRotate)
                    wheel.wheelTransform.localEulerAngles = Vector3.up * Mathf.LerpAngle(wheel.wheelTransform.localEulerAngles.y, _input.SteeringInput * 30f, Time.deltaTime * 10);
                wheel.wheelTransform.Rotate(Time.deltaTime * _view.Speed * 20 * Vector3.right, Space.Self);
            }
        }

        public virtual void FixedTick()
        {
            if (_view.IsGrounded)
            {
                if (_input.ThrottleInput > 0f)
                {
                    if (_view.Rigidbody.velocity.magnitude <= _view.MaxForwardVelocity)
                        _view.Rigidbody.velocity +=
                            _input.ThrottleInput * _view.ThrottlePrecision * _view.CarTransform.forward;
                }
                else if (_input.ThrottleInput < 0f)
                {
                    if (_view.Rigidbody.velocity.magnitude <= _view.MaxBackwardVelocity)
                        _view.Rigidbody.velocity +=
                            _input.ThrottleInput * _view.ThrottlePrecision * _view.CarTransform.forward;
                }

                if(_view.Rigidbody.velocity.magnitude >= 3f || _view.Rigidbody.velocity.magnitude <= -3f)
                    if(_view.Rigidbody.angularVelocity.magnitude <= _view.MaxAngularVelocity)
                        _view.Rigidbody.angularVelocity += _input.SteeringInput * _view.SteeringPrecision * Mathf.Sign(_input.ThrottleInput) * Vector3.up;
            }
            else
                _view.Rigidbody.AddForce(Vector3.up * _view.Downforce);
        }
    }
}