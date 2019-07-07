using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CarsTest
{
    public class SliderValueText : MonoBehaviour
    {
        public Slider slider;

        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Update()
        {
            _text.text = Math.Round(slider.value, 2).ToString();
        }
    }
}