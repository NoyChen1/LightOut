using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _winLoseText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _remainingTimeText;

    [SerializeField] private GameObject _statsPanel;
    [SerializeField] private Button _statsButton;
    [SerializeField] private Button _exitStats;

    private void Awake()
    {
        _statsPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
    }
    private void OnEnable()
    {
        GameEventDispatcher.OnGameOver += HandleGameOver;
        GameEventDispatcher.OnTimeUpdated += HandleTimeUpdated;
        _restartButton.onClick.AddListener(HandleRestartGame);
        _statsButton.onClick.AddListener(HandleStatsButtonClicked);
        _exitStats.onClick.AddListener(HandleExitStatsClicked);
    }

    private void OnDisable()
    {
        GameEventDispatcher.OnGameOver -= HandleGameOver;
        GameEventDispatcher.OnTimeUpdated -= HandleTimeUpdated;
        _restartButton.onClick.RemoveAllListeners();
        _statsButton.onClick.RemoveAllListeners();
        _exitStats.onClick.RemoveAllListeners();
    }

    private void HandleTimeUpdated(float remainingTime)
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        _remainingTimeText.text = $"{minutes:00}:{seconds:00}";
    }

    private void HandleRestartGame()
    {
        Debug.Log("Retry button clicked");
        _gameOverPanel.SetActive(false);
        GameEventDispatcher.TryAgainClicked();
    }

    private void HandleGameOver(bool gameState)
    {
        _gameOverPanel.SetActive(true);
        if (gameState)
        {
            _winLoseText.text = "You Won The Game !\n" +
                                "Want to Play Another Time ?";
            GameEventDispatcher.WinGame(gameState);
        }
        else
        {
            _winLoseText.text = "Time's Up !\n" +
                                 "Want to Try Another Time ?";

        }
    }

    private void HandleStatsButtonClicked()
    {
        _statsPanel.SetActive(true);
        _statsButton.gameObject.SetActive(false);
        GameEventDispatcher.StatsClicked();
    }

    private void HandleExitStatsClicked()
    {
        _statsButton.gameObject.SetActive(true);
        _statsPanel.gameObject.SetActive(false);
    }
}
