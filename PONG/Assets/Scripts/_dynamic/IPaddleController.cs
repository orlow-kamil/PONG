using UnityEngine;

public abstract class IPaddleController : Singleton<IPaddleController>
{
    #region Variables
    int _id;
    public int Id 
    { 
        get => _id; 
        set {_id = value;} 
    }
    protected GameManager _gameManager;
    protected PaddleSettings _paddleSettings;
	#endregion

    #region Non-Unity Methods
    void SetPaddleSettings(bool condition)
    {
        _paddleSettings.enabled = condition;
    }
    #endregion
    
}
