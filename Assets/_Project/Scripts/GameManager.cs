using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    [SerializeField] GameObject _qBertPrefab;
    [SerializeField] Vector3 _qbertStartPosition, _qBertStartRotation;


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
        set{
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

    //Unity Events
    public UnityEvent LivesChanged = new UnityEvent();
    public UnityEvent GameStateChanged = new UnityEvent();
    public Transform QBert => _qBert.transform;

    public BoardManager boardManager => _boardmanager; 

    private Transform _transform;
    BoardManager _boardmanager;
    QBert _qBert;
    Vector3 _qBertSpawnPosition, _qBertSpawnRotation;


    //Ensure Singleton only has one instance.
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

    void Update()
    {
        if (GameState != GameStates.Ready) return;
        if(Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        GameState = GameStates.GameStarted;
        Lives = 3;
        //set next extra life
        StartRound();
    }

    void StartRound()
    {
        SpawnQBert();
        GameState = GameStates.RoundStarted;
        // Start game music
    }

    void SpawnQBert()
    {
        if(!_qBert)
        {
            _qBert = Instantiate(_qBertPrefab, _transform).GetComponent<QBert>(); 
        }
        _qBert.ResetQBert(-_qBertSpawnPosition, _qBertSpawnRotation);
    }

    void ReadyToPlay()
    {
        //start play music
        Lives = 3;
        // set next extra life interval
        //reset score
        _boardmanager.SetUpBoard();
        ResetSpawnPosition();
        GameState = GameStates.Ready;
    }

    void ResetSpawnPosition(bool useLastPosition = false)
    {
        if(useLastPosition)
        {
            _qbertStartPosition = _qBert.transform.position;
            _qBertSpawnRotation = _qBert.GetComponent<Rigidbody>().eulerAngles;
            return;
        }

        _qBertSpawnPosition = _qbertStartPosition;
        _qBertSpawnRotation = _qBertStartRotation;
    }
}
