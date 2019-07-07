using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class NetworkInput: MonoBehaviour, IPunObservable
    {
        private CarInput _input;
        [Inject] public void Construct(CarInput input)
        {
            _input = input;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                _input.SteeringInput = (float) stream.ReceiveNext();
                _input.ThrottleInput = (float) stream.ReceiveNext();
            } else if (stream.IsWriting)
            {
                stream.SendNext(_input.SteeringInput);
                stream.SendNext(_input.ThrottleInput);
            }
        }
    }
}