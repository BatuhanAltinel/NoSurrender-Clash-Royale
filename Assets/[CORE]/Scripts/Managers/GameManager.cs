using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    ReadyToStart,
    OnTimer,
    InGame,
    End
}


public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameState _gameState = GameState.ReadyToStart;



    private void OnEnable() 
    {
        EventManager.OnGameStateChanged += SetGameState;        
    }

    private void OnDisable() 
    {
        EventManager.OnGameStateChanged -= SetGameState;
    }


    private void SetGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public bool IsGameState(GameState gameState)
    {
        return _gameState == gameState;
    }
}
