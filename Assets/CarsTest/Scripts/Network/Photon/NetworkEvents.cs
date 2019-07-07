using UnityEngine;

namespace CarsTest.Photon
{
    public class ManagerInstantiateSignal
    {
        public ManagerInstantiateSignal(int photonId)
        {
            PhotonId = photonId;
        }
        public int PhotonId { get; }
    }
    public class InputInstantiateSignal
    {
        public InputInstantiateSignal(int photonId)
        {
            PhotonId = photonId;
        }
        public int PhotonId { get; }
    }
    public class CarInstantiateSignal
    {
        public CarInstantiateSignal(Vector3 position, Quaternion rotation, int photonId, PlayerType carType, bool ai, int ownerActor)
        {
            Position = position;
            Rotation = rotation;
            PhotonId = photonId;
            CarType = carType;
            Ai = ai;
            OwnerActor = ownerActor;
        }

        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public int PhotonId { get; }
        public PlayerType CarType { get; }
        public bool Ai { get; }
        public int OwnerActor { get; }
    }
}