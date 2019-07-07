using System.Collections;
using System.Collections.Generic;
using CarsTest;
using UnityEngine;
using Zenject;

public class InitialGameState : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    [Inject]
    public void Constructor(GameStateManager gameStateManager, GameInfo gameInfo)
    {
        _gameStateManager = gameStateManager;
    }

    [SerializeField] private GameState initialGameState;

    private void Start()
    {
        _gameStateManager.ChangeState(initialGameState);
    }
}
