using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    [Header("Game Panel Elements")]
    [SerializeField] GameObject _gamePanel;
    float _gameTimer = 75;
    [SerializeField] TextMeshProUGUI _gameTimeText;


    [Header("End Panel Elements")]
    [SerializeField] GameObject _endPanel;

    [Header("Menu Panel")]
    [SerializeField] GameObject _menuPanel;
    [SerializeField] TextMeshProUGUI _startTimerText;
    [SerializeField] TextMeshProUGUI _GoText;
    int _startTimerCount = 3;


    void OnEnable()
    {
        EventManager.OnGameStateChanged += UpdateUI;
    }

    void OnDisable()
    {
        EventManager.OnGameStateChanged -= UpdateUI;
    }


    void Update()
    {
        if (GameManager.Instance.IsGameState(GameState.InGame))
            DecreaseGameTimer();
    }


    void UpdateUI(GameState state)
    {
        switch (state)
        {
            case GameState.ReadyToStart:
                ResetGameTimer();
                _menuPanel.gameObject.SetActive(CloseAllPanelExceptThis());
                break;

            case GameState.OnTimer:
                StartTimerSequence();
                break;

            case GameState.InGame:
                _gamePanel.gameObject.SetActive(CloseAllPanelExceptThis());
                break;

            case GameState.End:
                _endPanel.gameObject.SetActive(CloseAllPanelExceptThis());
                break;
        }
    }


    void StartTimerSequence()
    {
        _startTimerText.gameObject.SetActive(true);
        _startTimerText.text = _startTimerCount.ToString();


        if (_startTimerCount > 0)
            _startTimerText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                _startTimerText.transform.localScale = Vector3.zero;
                MinusStartTimer();
            });
        else
        {
            _startTimerText.gameObject.SetActive(false);
            _startTimerText.transform.localScale = Vector3.zero;
            _startTimerCount = 3;

            ShowGoText();
        }
    }


    void MinusStartTimer()
    {
        _startTimerCount--;
        StartTimerSequence();
    }

    void ShowGoText()
    {
        _GoText.gameObject.SetActive(true);

        _GoText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _GoText.gameObject.SetActive(false);
            _GoText.transform.localScale = Vector3.zero;

            EventManager.OnGameStateChanged(GameState.InGame);
        });
    }


    void DecreaseGameTimer()
    {
        if (_gameTimer <= 0)
            StartCoroutine(WaitForEndState());
        // EventManager.OnGameStateChanged(GameState.End);

        _gameTimer -= Time.deltaTime;

        int minute = (int)_gameTimer / 60;
        int second = 0;

        if (minute > 0)
            second = (int)_gameTimer - 60;
        else
            second = (int)_gameTimer;

        _gameTimeText.text = second.ToString($"{minute}:00");
    }


    void ResetGameTimer()
    {
        _gameTimer = 75;
    }



    bool CloseAllPanelExceptThis()
    {
        _menuPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(false);

        return true;
    }
   


    IEnumerator WaitForEndState()
    {
        //InputManager.Instance.InputEnable = false;

        yield return new WaitForSeconds(1f);
        EventManager.OnGameStateChanged(GameState.End);

        yield return new WaitForSeconds(2f);
        //InputManager.Instance.InputEnable = true;
    }

}
