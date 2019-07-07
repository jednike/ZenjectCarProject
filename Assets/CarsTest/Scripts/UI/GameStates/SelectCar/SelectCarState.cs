using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class SelectCarState : GameStateEntity
    {
        [SerializeField] private Transform carsTransform;
    
        private CarCard _currentCard;
        private CarsInfo _carsInfo;
        private CarCard.Factory _factory;

        [Inject]
        public void Constructor(GameStateManager gameStateManager, GameInfo gameInfo, CarsInfo carsInfo, CarCard.Factory factory)
        {
            base.Constructor(gameStateManager, gameInfo);
            _carsInfo = carsInfo;
            _factory = factory;
        }

        public override void Start()
        {
            var i = 0;
            foreach (var car in _carsInfo.CarInfos)
            {
                var carCard = _factory.Create();
                carCard.transform.SetParent(carsTransform);
//                carCard.SetInfo(car, i, ActionOnClick);
//                carCard.Select(false);
//            
                if (!_currentCard || i == Scores.Instance.SelectedCar)
                    _currentCard = carCard;
                i++;
            }
//            _currentCard.Select(true);
        }

        private void ActionOnClick(CarCard card)
        {
            if(_currentCard == card)
                return;
            _currentCard.Select(false);
            _currentCard = card;
            _currentCard.Select(true);
        }
        
        
        public class Factory : PlaceholderFactory<SelectCarState>
        {
        }
    }
}
