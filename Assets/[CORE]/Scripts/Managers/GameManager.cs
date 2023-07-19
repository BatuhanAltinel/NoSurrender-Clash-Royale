using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    ReadyToStart,
    InGame,
    End
}


public class GameManager : Singleton<GameManager>
{
    GameState _gameState = GameState.ReadyToStart;



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
