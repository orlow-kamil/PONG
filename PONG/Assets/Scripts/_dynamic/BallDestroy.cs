using UnityEngine;

public class BallDestroy : MonoBehaviour
{
	#region Variables
    GameManager _gameManager;
    AudioSource _audioSource;
    int _id;
	#endregion

	#region Unity Methods
    void Awake() 
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager did't find.");
        }

       _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("The Audio Source in " + gameObject.name + " is missing.");
        }
    }

    void Start() 
    {
        _id = (transform.position.x > 0) ? 0 : 1;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<Rigidbody2D>())
        {
            Destroy(other.gameObject);
            _audioSource.Play();
            _gameManager.IncreasePlayerScore(_id);
            _gameManager.IfBallExist = false;
        }
    }	
	#endregion
}
