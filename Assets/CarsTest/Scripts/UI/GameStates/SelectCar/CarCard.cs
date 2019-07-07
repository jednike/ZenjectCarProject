using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class CarCard : MonoBehaviour
    {
        [SerializeField] private GameObject activePanel;
        [SerializeField] private Image portraitImage;
        [SerializeField] private Text nameText;

        private int _index;

        public void SetInfo(CarInfo carInfo, int index, Action<CarCard> actionOnClick)
        {
            _index = index;

            portraitImage.sprite = carInfo.Image;
            nameText.text = carInfo.Name;
        
        
            GetComponent<Button>().onClick.AddListener(() => { actionOnClick(this); });
        }
    
        public void Select(bool select)
        {
            activePanel.SetActive(select);
            if(select)
                Scores.Instance.SelectedCar = _index;
        }

        public class Factory: PlaceholderFactory<CarCard>
        {
        }
    }
}