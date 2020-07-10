using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCollision : MonoBehaviour
{
    #region Variables
    [SerializeField] float _newBallVelY;
    [SerializeField] bool ifIsBarUp;
    #endregion

    #region Unity Methods
    void Start() 
    {
        _newBallVelY = (ifIsBarUp) ? -_newBallVelY : _newBallVelY;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Rigidbody2D ballRgb2D = other.gameObject.GetComponent<Rigidbody2D>();
        if(ballRgb2D == null)
        {
            return;
        }

        if(ballRgb2D.velocity.y > -0.1f && ballRgb2D.velocity.y < 0.1f)
        {
            ballRgb2D.velocity = new Vector2(ballRgb2D.velocity.x, _newBallVelY);
        }
    }
    #endregion
}
