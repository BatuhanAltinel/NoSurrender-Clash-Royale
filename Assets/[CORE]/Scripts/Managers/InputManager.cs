using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool InputEnable { get; set; }

    void Start()
    {
        InputEnable = true;
    }

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetMouseButton(0) && InputEnable)
        {

            if ((GameManager.Instance.IsGameState(GameState.ReadyToStart)))
            {
                EventManager.OnGameStateChanged.Invoke(GameState.OnTimer);
            }
            else if (GameManager.Instance.IsGameState(GameState.End))
            {
                EventManager.OnGameStateChanged.Invoke(GameState.ReadyToStart);
            }


        }
    }
}
