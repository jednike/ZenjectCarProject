using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class ChaserFacade : CarFacade, IPoolable<IMemoryPool>, IDisposable
    {
        private ChaserRegistry _registry;
        private IMemoryPool _pool;

        [Inject] public void Construct(CarView view, CarDeathHandler deathHandler, ChaserRegistry registry)
        {
            base.Construct(view, deathHandler);
            _registry = registry;
            View.CarTransform.tag = "Chaser";
        }
        
        public void OnDespawned()
        {
            _registry.RemoveChaser(this);
            _pool = null;
        }
        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            View.Rotation = Quaternion.identity;
            View.Velocity = Vector3.zero;
            _registry.AddChaser(this);
        }
        public void Dispose()
        {
            _pool?.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<ChaserFacade>
        {
        }
    }
}