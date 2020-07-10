using UnityEngine;

public class PlayerController : IPaddleController
{
	#region Variables
    [SerializeField] private string _axis;
    public string Axis 
    { 
        get => _axis; 
        set {_axis = value;} 
    }
	#endregion

	#region Non-Unity Methods
    void SetupPlayer()
    {
        _paddleSettings.SetPaddle(Id);
    }
	#endregion

	#region Unity Methods
    protected override void Awake() 
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager did't find.");
        }

       _paddleSettings = GetComponent<PaddleSettings> ();
        if(_paddleSettings == null)
        {
            Debug.LogError("The PaddleSettings in " + gameObject.name + " is missing.");
        }
    }
    void Start()
    {
        SetupPlayer();
    }

    void Update()
    {
        _paddleSettings.Move(_gameManager.PlayerSpeed);
        _paddleSettings.BoundaryPosPlayer(_gameManager.BoundariesPos[0].position.y, _gameManager.BoundariesPos[1].position.y);
    }	
	#endregion
}
