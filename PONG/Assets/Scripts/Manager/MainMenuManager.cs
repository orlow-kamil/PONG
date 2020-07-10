using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Variables
    [Header("Canvas Stats")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _creditsMenu;
    #endregion

    #region Non-Unity Methods 
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits(bool condition)
    {
        _creditsMenu.SetActive(condition);
        _mainMenu.SetActive(!condition);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");
        Application.Quit();
    }
    #endregion

    #region Unity Methods  
    private void Start() 
    {
        ShowCredits(false);
    }	
	#endregion
}
