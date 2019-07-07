using System;
using System.Collections;
using System.Collections.Generic;
using CarsTest;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class NetworkBehaviour: IInitializable
{
    private readonly GameStateManager _stateManager;
    private readonly NetworkConnector _networkConnector;
    private readonly SignalBus _signalBus;

    public NetworkBehaviour(GameStateManager stateManager, SignalBus signalBus, NetworkConnector networkConnector)
    {
        _networkConnector = networkConnector;
        _stateManager = stateManager;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateChangedSignal stateChange)
    {
        switch (stateChange.NewState)
        {
            case GameState.None:
                break;
            case GameState.Menu:
                if (stateChange.LastState == GameState.GamePlay || stateChange.LastState == GameState.EndGame || stateChange.LastState == GameState.Pause)
                    _networkConnector.Disconnect();
                break;
        }
    }
}
