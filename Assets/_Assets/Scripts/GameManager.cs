using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Игровой процесс

    public static GameManager Instance {  get; private set; }

    public event EventHandler OnStatedChanged; //Функция при изменения состояния
    public event EventHandler OnGamePause; //Функция оставки игры (для визуализации)
    public event EventHandler OnGameUnPause; //Функция оставки игры (для визуализации)
    private enum State //Состояние игры
    {
        WaittingToStart,
        CounterDownToStart,
        GamePlaying,
        GameOver,
    }

    private State _state;

    private float _waitingToStartTimer = 1f; //Таймер ожидания игры
    private float _сounterDownToStartTimer = 3f; //Таймер начала игры
    private float _gameToStartTimer; //Таймер ожидания игры
    private float _gameToStartTimerMax = 60; //Таймер максимального времени игры
   // private bool _isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        _state = State.WaittingToStart;
    }

    private void Update()
    {
        switch (_state)  //Переходы между состояниями
        {
            case State.WaittingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer < 0f)
                {
                    _state = State.CounterDownToStart;
                    OnStatedChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CounterDownToStart:
                _сounterDownToStartTimer -= Time.deltaTime;
                if (_сounterDownToStartTimer < 0f)
                {
                    _state = State.GamePlaying;
                    _gameToStartTimer = _gameToStartTimerMax;
                    OnStatedChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                _gameToStartTimer -= Time.deltaTime;
                if (_gameToStartTimer < 0f)
                {
                    _state = State.GameOver;
                    OnStatedChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

                case State.GameOver:
                break;
        }
    }

    public void ReduceGameTime(float seconds) //Функция для уменьшения времени игры
    {
        _gameToStartTimer -= seconds;
        if (_gameToStartTimer < 0f)
        {
            _gameToStartTimer = 0f;
            _state = State.GameOver;
            OnStatedChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void IncreaseGameTime(float seconds) //Функция для увелечения времени игры
    {
        _gameToStartTimer += seconds;
    }


    public bool IsGamePlaing()
    {
        return _state == State.GamePlaying; //Функция позволяет играть в данном режиме
    }

    public bool IsCountStartActive()
    {
        return _state == State.CounterDownToStart;
    }
    public float GetCountStartTimer() //Функция для обратного отсчета
    {
        return _сounterDownToStartTimer;
    }

    public bool IsGameOver() //Функция для завершения игры
    {
        return _state == State.GameOver;
    }

    public float GetGamePlayingTimer()
    {
       return _gameToStartTimer;
    }

    public float GetGamePlayingTimerNormalized() //Функция максимального времени игры
    {
        return 1- (_gameToStartTimer/_gameToStartTimerMax);
    }

    public void PauseGame() //Функция для паузы игры
    {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
     }

    public void ResumeGame() //Функция для паузы игры
    {
        Time.timeScale = 1f;

            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        
    }
}
