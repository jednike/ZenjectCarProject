using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private ParticleSystem particleSystems;
        
        private float _startTime;
        private IMemoryPool _pool;

        public void Update()
        {
            if (Time.realtimeSinceStartup - _startTime > lifeTime)
            {
                _pool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
        }

        public void OnSpawned(IMemoryPool pool)
        {
            particleSystems.Clear();
            particleSystems.Play();

            _startTime = Time.realtimeSinceStartup;
            _pool = pool;
        }

        public class Factory : PlaceholderFactory<Explosion>
        {
        }
    }
}