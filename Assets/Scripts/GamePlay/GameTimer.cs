using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

public enum State
{
    Running,
    Stopped
}
public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 120f;
    [SerializeField] private float _remainingTime;
    private State _timerState;

    public float TimeTaken => _timeLimit - _remainingTime;

    private void OnEnable()
    {
        GameEventDispatcher.OnGameOver += StopTimer;
    }

    private void OnDisable()
    {
        GameEventDispatcher.OnGameOver -= StopTimer;
    }

    public void StartTimer()
    {
        _remainingTime = _timeLimit;
        _timerState = State.Running;
        RunTimerAsync().Forget();
    }

    public void StopTimer(bool state)
    {
        _timerState = State.Stopped;
    }

    private async UniTaskVoid RunTimerAsync()
    {
        while (_timerState == State.Running && _remainingTime > 0f)
        {
            await UniTask.DelayFrame(1);
            _remainingTime -= Time.deltaTime;
            GameEventDispatcher.TimerUpdated(_remainingTime);
        }

        if (_timerState == State.Running)
        {
            _timerState = State.Stopped;
            GameEventDispatcher.TimerOver();
        }
    }
}
