using System;
using System.Collections;
using System.Collections.Generic;
using CarsTest;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneBehaviour: IInitializable
{
    private readonly ZenjectSceneLoader _sceneLoader;
    private readonly GameStateManager _stateManager;
    private readonly SignalBus _signalBus;

    public SceneBehaviour(ZenjectSceneLoader sceneLoader, GameStateManager stateManager, SignalBus signalBus)
    {
        _sceneLoader = sceneLoader;
        _stateManager = stateManager;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateChangedSignal stateChange)
    {
        switch (stateChange.LastState)
        {
            case GameState.SelectCar:
            case GameState.SelectLevel:
            case GameState.SelectType:
            case GameState.EndGame:
            case GameState.Pause:
                SceneManager.UnloadSceneAsync(stateChange.LastState.ToString());
                break;
            case GameState.Menu:
            case GameState.GamePlay:
                if(stateChange.NewState == GameState.Loading || stateChange.NewState == GameState.EndGame)
                    SceneManager.UnloadSceneAsync(stateChange.LastState.ToString());
                break;
        }
        switch (stateChange.NewState)
        {
            case GameState.None:
                break;
            case GameState.Menu:
                if(stateChange.LastState == GameState.GamePlay || stateChange.LastState == GameState.EndGame || stateChange.LastState == GameState.Pause)
                    _sceneLoader.LoadScene(stateChange.NewState.ToString());
                break;
            case GameState.SelectCar:
            case GameState.SelectLevel:
            case GameState.SelectType:
            case GameState.EndGame:
            case GameState.Pause:
            case GameState.Loading:
                _sceneLoader.LoadScene(stateChange.NewState.ToString(), LoadSceneMode.Additive);
                break;
            case GameState.GamePlay:
                if(stateChange.LastState != GameState.Pause)
                    _sceneLoader.LoadScene(stateChange.NewState.ToString(), LoadSceneMode.Additive);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
