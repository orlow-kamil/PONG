using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallBehaviour : Singleton<BallBehaviour>
{
	#region Variables
    Rigidbody2D _rgb2D;
    GameManager _gameManager;
    AudioSource _audioSource;
    
    [Header("Audio clip")]
    [SerializeField] private AudioClip _wallHitAudio;
    [SerializeField] private AudioClip _playerHitAudio;

    [Space(2)]
    [Header("Stats")]
    [SerializeField] float _sensivity;
	#endregion

	#region Non-Unity Methods
    Vector2 RandomGeneratorVector2() 
    {
        float x = (Random.value > 0.5f) ? 1 : -1;
        float y = Random.Range(-.9f, .9f);
        return new Vector2(x, y);
    }

    public void PushBall(float power) 
    {
        if(_rgb2D == null)
        {
            Debug.LogWarning(gameObject.name + " -> Missing Rigidbody2D");
            return;  
        } 

        _rgb2D.velocity = RandomGeneratorVector2() * power;
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
        
        _rgb2D = GetComponent<Rigidbody2D> ();
        if(_rgb2D == null)
        {
            Debug.LogError("The Rigidbody2D in " + gameObject.name + " is missing.");
        }

        _audioSource = GetComponent<AudioSource> ();
        if(_audioSource == null)
        {
            Debug.LogError("The Audio Source in " + gameObject.name + " is missing.");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        PaddleSettings paddle = other.gameObject.GetComponent<PaddleSettings>();
        if(paddle == null)
        {
            return;
        }

        float playerVelocityY = paddle.ObjVelocity.y / 30f;
        if(playerVelocityY > -_sensivity && playerVelocityY < _sensivity)
        {
            return;
        }

        //Think about how much force the pallet hit a ball
        _rgb2D.AddForce(new Vector2(-playerVelocityY, 0.0f) * _gameManager.Ballpower);      
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        _gameManager.IncreaseBallPower();
        if(other.gameObject.CompareTag("Player")) 
        {
            _audioSource.PlayOneShot(_playerHitAudio);
            return;
        }

        _audioSource.PlayOneShot(_wallHitAudio);
    }	
	#endregion
}
