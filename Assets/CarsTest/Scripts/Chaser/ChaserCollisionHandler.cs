using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class ChaserCollisionHandler: MonoBehaviour
    {
        private float _lifeTime = 0f;
        private ChaserFacade _chaserFacade;
        [Inject]
        public void Construct(ChaserFacade chaserFacade)
        {
            _chaserFacade = chaserFacade;
        }

        private void Update()
        {
            if(_lifeTime < 2f)
                _lifeTime += Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(_lifeTime < 2f)
                return;

            if (other.transform.CompareTag("Road") || !(other.relativeVelocity.magnitude > 20f)) return;
            _lifeTime = 0f;
            _chaserFacade.Die();
        }
    }
}