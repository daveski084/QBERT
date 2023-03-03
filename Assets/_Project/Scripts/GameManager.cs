using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameStates
    {
        Ready,
        GameStarted,
        RoundStarted,
        QbertDied,
        LevelComplete,
        Gameover
    }

    private GameStates _gameState;

    public GameStates GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            GameStateChanged.Invoke();
        }
    }

    public bool isPlaying => GameState == GameStates.RoundStarted;

    private int _lives;

    public int Lives
    {
        get => _lives;

        private set
        {
            _lives = value;
            LivesChanged.Invoke(); 
        }
    }

    public UnityEvent LivesChanged = new UnityEvent();
    public UnityEvent GameStateChanged = new UnityEvent();


    private Transform _transform;
    BoardManager _boardmanager;

    private void Awake()
    {
        if(Instance != null && this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _transform = transform;
        _boardmanager = GetComponent<BoardManager>();
    }

    private void Start()
    {
        ReadyToPlay(); 
    }

    void ReadyToPlay()
    {
        //start play music
        Lives = 3;
        // set next extra life interval
        //reset score
        _boardmanager.SetUpBoard();
        // reset QBERT's position
        GameState = GameStates.Ready;
    }
}
