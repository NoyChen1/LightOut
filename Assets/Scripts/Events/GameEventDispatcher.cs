using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDispatcher : MonoBehaviour
{
    public static event Action<Vector2Int> OnTileClicked;
    public static event Action<bool> OnGameOver;
    public static event Action OnTryAgainClicked;
    public static event Action OnTimeOver;
    public static event Action<float> OnTimeUpdated;
    public static event Action<bool> OnWin;
    public static event Action OnStatsClicked;
    public static void TileClicked(Vector2Int tile)
    {
        OnTileClicked?.Invoke(tile); 
    }
    public static void GameOver(bool gameState)
    {
        OnGameOver?.Invoke(gameState);
    }

    public static void TryAgainClicked()
    {
        OnTryAgainClicked?.Invoke();
    }

    public static void TimerOver()
    {
        OnTimeOver?.Invoke();
    }

    public static void TimerUpdated(float time)
    {
        OnTimeUpdated?.Invoke(time);
    }

    public static void WinGame(bool gameState)
    {
        OnWin?.Invoke(gameState);
    }

    public static void StatsClicked()
    {
        OnStatsClicked?.Invoke();
    }
}
