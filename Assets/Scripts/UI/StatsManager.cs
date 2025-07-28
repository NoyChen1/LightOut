using TMPro;
using UnityEngine;
using Zenject;

public class StatsManager : MonoBehaviour
{
    [Inject] private GameTimer _gameTimer;
    private StatsData _data;

    [SerializeField] private TextMeshProUGUI _attemptsTxt;
    [SerializeField] private TextMeshProUGUI _winStatsTxt;
    [SerializeField] private TextMeshProUGUI _averageTxt;


    private const string AttemptsKey = "Stats_Attempts";
    private const string WinsKey = "Stats_Wins";
    private const string TotalTimeKey = "Stats_TotalTime";

    private void OnEnable()
    {
        _data = StatsSaveSystem.LoadStats();
        GameEventDispatcher.OnGameOver += RecordGame;
        GameEventDispatcher.OnStatsClicked += HandleStatsClicked;
    }

    private void OnDisable()
    {
        GameEventDispatcher.OnGameOver -= RecordGame;
        GameEventDispatcher.OnStatsClicked -= HandleStatsClicked;
    }

    public void RecordGame(bool isWin)
    {
        
        float timeTaken = _gameTimer.TimeTaken;
        /*
        int attempts = PlayerPrefs.GetInt(AttemptsKey, 0);
        int wins = PlayerPrefs.GetInt(WinsKey, 0);
        float totalTime = PlayerPrefs.GetFloat(TotalTimeKey, 0f);

        attempts++;
        if (isWin) wins++;
        totalTime += timeTaken;
        */

        _data.attempts++;
        if(isWin) _data.wins++;
        _data.totalTime += timeTaken;
        StatsSaveSystem.SaveStats(_data);

        /*
        PlayerPrefs.SetInt(AttemptsKey, attempts);
        PlayerPrefs.SetInt(WinsKey, wins);
        PlayerPrefs.SetFloat(TotalTimeKey, totalTime);
        PlayerPrefs.Save();
        */
    }

    private void HandleStatsClicked()
    {
        float successRate = GetSuccessRate();
        float averageTime = GetAverageTime();

        _attemptsTxt.text = $"Games Played: {_data.attempts}";
        _winStatsTxt.text = $"Win Rate: {(int)successRate}%";
        _averageTxt.text = $"Avg. GameTime: {averageTime.ToString("F2")} seconds";
    }
    public float GetSuccessRate()
    {
        /*
        int attempts = PlayerPrefs.GetInt(AttemptsKey, 0);
        int wins = PlayerPrefs.GetInt(WinsKey, 0);
        */

        if (_data.attempts == 0) return 0f;
        return (float) _data.wins / _data.attempts * 100f;
    }

    public float GetAverageTime()
    {
        /*
        int attempts = PlayerPrefs.GetInt(AttemptsKey, 0);
        float totalTime = PlayerPrefs.GetFloat(TotalTimeKey, 0f);
        */


        if (_data.attempts == 0) return 0f;
        return _data.totalTime / _data.attempts;
    }

    public void ResetStats()
    {
        StatsSaveSystem.ResetStats();
        _data = new StatsData();
        /*
        PlayerPrefs.DeleteKey(AttemptsKey);
        PlayerPrefs.DeleteKey(WinsKey);
        PlayerPrefs.DeleteKey(TotalTimeKey);
        */
    }
}
