using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class GameStateEntity : MonoBehaviour, IDisposable
    {
        protected GameInfo GameInfo;
        protected GameStateManager GameStateManager;
        [Inject]
        public virtual void Constructor(GameStateManager gameStateManager, GameInfo gameInfo)
        {
            GameInfo = gameInfo;
            GameStateManager = gameStateManager;
        }

        private void Awake()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}