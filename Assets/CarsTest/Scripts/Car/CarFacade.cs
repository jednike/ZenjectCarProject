using UnityEngine;
using Zenject;

namespace CarsTest
{
    public interface ICarFacade
    {
        void Die();
    }
    public class CarFacade: MonoBehaviour, ICarFacade
    {
        protected CarView View;
        protected CarDeathHandler DeathHandler;

        [Inject] public virtual void Construct(CarView view, CarDeathHandler deathHandler)
        {
            View = view;
            DeathHandler = deathHandler;
        }

        public Transform Transform => View.CarTransform;
        public Vector3 Position
        {
            get => View.Position;
            set => View.Position = value;
        }
        public Quaternion Rotation
        {
            get => View.Rotation;
            set => View.Rotation = value;
        }

        public void Die()
        {
            DeathHandler.Die();
        }
    }
}