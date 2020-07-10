using UnityEngine;

public class PaddleSettings : MonoBehaviour
{
    #region Variables
    SpriteRenderer _sprite; 
    string _axis;
    Vector2 _prevPos;
    Vector2 _newPos;
    Vector2 _objVelocity;
    public Vector2 ObjVelocity 
    { 
        get => _objVelocity; 
    }
	#endregion

	#region Non-Unity Methods
    public void SetPaddle(int id)
    {
        _sprite = GetComponent<SpriteRenderer> ();
        _axis = (id == 0) ? "Vertical1" : "Vertical2";
    }

    public void BoundaryPosPlayer(float borderDown, float borderUp)
    {
        float sizeY = _sprite.bounds.size.y / 2;
        Vector2 pos = transform.position;
        pos.y = Mathf.Clamp(transform.position.y, borderDown + sizeY, borderUp - sizeY);
        transform.position = pos;
    }
    public void Move(float moveSpeed)
    {
        Vector3 move = new Vector2(0, Input.GetAxis(_axis));
        transform.position += move * moveSpeed * Time.deltaTime;
    }	
	#endregion

    #region Unity Methods
    void Start() 
    {
        _prevPos = transform.position;
        _newPos = transform.position;
    }

    void FixedUpdate() 
    {
        _newPos = transform.position;
        _objVelocity = (_newPos - _prevPos) / Time.fixedDeltaTime;
        _prevPos = _newPos;
    }
    #endregion
}
