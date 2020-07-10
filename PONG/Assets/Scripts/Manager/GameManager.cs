using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Variables
    UIManager _uIManager;

    [Header("Game elements")]
    [SerializeField] private Transform _dynamicObjectsContainer;
    [SerializeField] private List<Transform> _boundariesPos;
    public List<Transform> BoundariesPos
    { 
        get => _boundariesPos;
    }
    [SerializeField] private List<Transform> _startPlayerPos;
    [SerializeField] private Transform _startBallPos;
    List<GameObject> _currPlayers;
    GameObject _currBall;
    public GameObject CurrBall 
    { 
        get => _currBall;
    }

    [Space(2)]
    [Header("Player/Computer stats")]
    [SerializeField] private GameObject _paddlePrefab;
    [SerializeField][Range(2 , 20)] private float _playerSpeed;
    public float PlayerSpeed 
    { 
        get => _playerSpeed;
    }

    [Space(2)]
    [Header("Ball stats")]
    [SerializeField] private GameObject _prefabBall;
    [SerializeField][Range(1, 5)] private float _startBallPower;  
    float _ballPower;
    public float Ballpower 
    { 
        get => _ballPower; 
        set {_ballPower = value;} 
    }
    [SerializeField] private float _ballDelayTime = 1.0f;

    [Space(2)]
    [Header("Game stats")]
    [SerializeField] private int[] _scores;
    int maxScore = 10;
    [SerializeField] private KeyCode _pauseKeyCode = KeyCode.P;

    bool ifBallExist;
    public bool IfBallExist 
    { 
        get  => ifBallExist; 
        set {ifBallExist = value;} 
    }

    bool ifIsPaused;
    bool ifGameIsEnd;
    public bool IfGameIsEnd 
    { 
        get => ifGameIsEnd; 
        set {ifGameIsEnd = value;} 
    }
	#endregion

	#region Non-Unity Methods
    public void IncreaseBallPower()
    {
        _ballPower += 0.25f;
    }

    public void IncreasePlayerScore(int id)
    {
        if(id == 0 || id == 1) 
        {
            _scores[id] ++;
            _uIManager.SetScoreImage(id, _scores[id]);
        }
    }

    void ScoresSetup(int id)
    {
        _scores[id] = 0;
        _uIManager.SetScoreImage(id, _scores[id]);
    }

    void PlayerSetup(int id)
    {
        ScoresSetup(id);

        GameObject currPlayer = Instantiate(_paddlePrefab, _startPlayerPos[id].position, _startPlayerPos[id].rotation);
        currPlayer.name = "Player_" + (id + 1).ToString();
        currPlayer.gameObject.transform.SetParent(_dynamicObjectsContainer);

        currPlayer.AddComponent<PlayerController>();
        currPlayer.GetComponent<PlayerController>().enabled = true;
        currPlayer.GetComponent<PlayerController>().Id = id;
        currPlayer.GetComponent<PlayerController>().Axis = (id == 0) ? "Vertical1" : "Vertical2";

        _currPlayers.Add(currPlayer);
    }

    void StopPlayer(int id)
    {
        _currPlayers[id].GetComponent<IPaddleController>().enabled = false;
    }

    void PauseGame()
    {
        ifIsPaused = !ifIsPaused;
        Time.timeScale = (ifIsPaused) ? 0.0f : 1.0f;
        _uIManager.ShowPauseText(ifIsPaused);
    }
    void ResetGame()
    {
        PauseGame();
        ifGameIsEnd = false;
        ifBallExist = false;

        if(_currBall != null)
        {
            Destroy(_currBall);
            _currBall = null;
        }

        if(_currPlayers != null)
        {
            foreach (GameObject player in _currPlayers)
            {
                Destroy(player);
            }
            _currPlayers = null;
        }
    }

    void PlayNewGame()
    {
        ResetGame();

        _scores = new int[2];
        _currPlayers = new List<GameObject>();
        PlayerSetup(0);
        PlayerSetup(1);

        StartCoroutine(GameController());
    }
	#endregion

	#region Unity Methods
    void Awake() 
    {
        _uIManager = GetComponent<UIManager>();
        if(_uIManager == null)
        {
            Debug.LogError("The UI Manager is missing.");
        }
    }

    void Start() 
    {
        PlayNewGame();
    }

    void Update() 
    {
        if(Input.GetKeyDown(_pauseKeyCode))
        {
            PauseGame();
        }
    }

    IEnumerator BallController()
    {
        while(true)
        {
            yield return new WaitForSeconds(_ballDelayTime);
            if(!ifBallExist && !ifGameIsEnd && _currBall == null)
            {
                // Debug.Log("New ball");
                _currBall = Instantiate(_prefabBall, _startBallPos.position, _startBallPos.rotation);
                _currBall.name = "Ball";
                _currBall.gameObject.transform.SetParent(_dynamicObjectsContainer);
                _ballPower = _startBallPower;
                yield return new WaitUntil(() => _currBall.GetComponent<Rigidbody2D>() != null);
                _currBall.GetComponent<BallBehaviour>().PushBall(_ballPower);
                ifBallExist = true;
            }
        }
    }

    IEnumerator GameController()
    {
        // Debug.Log("Start Game");
        StartCoroutine(BallController());
        while(!ifGameIsEnd)
        {
            if(_scores[0] >= maxScore || _scores[1] >= maxScore)
            {
                if(_scores[0] > _scores[1])
                {
                    _uIManager.ShowWinnerText(0);
                }
                else
                {
                    _uIManager.ShowWinnerText(1);
                }
                StopCoroutine(BallController());
                ifGameIsEnd = true;
                for (int i = 0; i < _currPlayers.Count; i++)
                {
                    StopPlayer(i);
                }
            }
            yield return null;
        }
    }
	#endregion
}
