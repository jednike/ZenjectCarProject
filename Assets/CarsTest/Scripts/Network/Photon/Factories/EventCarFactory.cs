using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest.Photon
{
    public class EventCarFactory: IInitializable
    {
        private readonly SignalBus _signalBus;
        public EventCarFactory(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CarInstantiateSignal>(OnCarInstantiated);
        }

        protected virtual void OnCarInstantiated(CarInstantiateSignal carInstantiateSignal)
        {
            
        }

        protected void SetViewId(Transform carTransform, CarInstantiateSignal carInstantiateSignal)
        {
            carTransform.position = carInstantiateSignal.Position;
            carTransform.rotation = carInstantiateSignal.Rotation;
            carTransform.GetComponent<PhotonView>().ViewID = carInstantiateSignal.PhotonId;
        }
    }
}