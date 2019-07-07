using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class EndGameState : GameStateEntity
    {
        [SerializeField] private Text endGameText;
        [SerializeField] private Text endGameTextSmall;
        [Inject] private readonly GameInfo _gameInfo;

        private void Awake()
        {
            endGameTextSmall.text = _gameInfo.win ? "Runner win" : "Runner died";
        }
    }
}