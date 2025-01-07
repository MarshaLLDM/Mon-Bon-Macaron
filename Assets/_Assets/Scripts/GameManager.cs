using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //������� �������

    public static GameManager Instance {  get; private set; }

    public event EventHandler OnStatedChanged; //������� ��� ��������� ���������
    public event EventHandler OnGamePause; //������� ������� ���� (��� ������������)
    public event EventHandler OnGameUnPause; //������� ������� ���� (��� ������������)
    private enum State //��������� ����
    {
        WaittingToStart,
        CounterDownToStart,
        GamePlaying,
        GameOver,
    }

    private State _state;

    private float _waitingToStartTimer = 1f; //������ �������� ����
    private float _�ounterDownToStartTimer = 3f; //������ ������ ����
    private float _gameToStartTimer; //������ �������� ����
    private float _gameToStartTimerMax = 60; //������ ������������� ������� ����
   // private bool _isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        _state = State.WaittingToStart;
    }

    private void Update()
    {
        switch (_state)  //�������� ����� �����������
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
                _�ounterDownToStartTimer -= Time.deltaTime;
                if (_�ounterDownToStartTimer < 0f)
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

    public void ReduceGameTime(float seconds) //������� ��� ���������� ������� ����
    {
        _gameToStartTimer -= seconds;
        if (_gameToStartTimer < 0f)
        {
            _gameToStartTimer = 0f;
            _state = State.GameOver;
            OnStatedChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void IncreaseGameTime(float seconds) //������� ��� ���������� ������� ����
    {
        _gameToStartTimer += seconds;
    }


    public bool IsGamePlaing()
    {
        return _state == State.GamePlaying; //������� ��������� ������ � ������ ������
    }

    public bool IsCountStartActive()
    {
        return _state == State.CounterDownToStart;
    }
    public float GetCountStartTimer() //������� ��� ��������� �������
    {
        return _�ounterDownToStartTimer;
    }

    public bool IsGameOver() //������� ��� ���������� ����
    {
        return _state == State.GameOver;
    }

    public float GetGamePlayingTimer()
    {
       return _gameToStartTimer;
    }

    public float GetGamePlayingTimerNormalized() //������� ������������� ������� ����
    {
        return 1- (_gameToStartTimer/_gameToStartTimerMax);
    }

    public void PauseGame() //������� ��� ����� ����
    {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
     }

    public void ResumeGame() //������� ��� ����� ����
    {
        Time.timeScale = 1f;

            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        
    }
}
