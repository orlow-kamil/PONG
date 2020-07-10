using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
	#region Variables
    [Header("UI elements")]
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private List<Image> _playersScore;

    [Space(2)]
    [Header("Numbers stats")]
    [SerializeField] private List<Sprite> _numbersSprite;
    [SerializeField] private float _oneScoreWidth;
    Vector2 _normalSize = new Vector2();
	#endregion

	#region Non-Unity Methods
    public void SetScoreImage(int id, int score)
    {
        if(score >= 10)
        {
            return;
        }

        _playersScore[id].sprite = _numbersSprite[score];
        if(score == 1)
        {
            _playersScore[id].GetComponent<RectTransform>().sizeDelta = new Vector2(_oneScoreWidth, _normalSize.y);
        }
        else
        {
            _playersScore[id].GetComponent<RectTransform>().sizeDelta = _normalSize;
        }
    }

    public void ShowPauseText(bool condition)
    {
       _pauseText.enabled = condition;
    }

    public void ShowWinnerText(int id)
    {
        if(id == 0)
        {
            _winnerText.text = "Winner is Player 1";
            _winnerText.enabled = true;
        }
        else if(id == 1)
        {
            _winnerText.text = "Winner is Player 2";
            _winnerText.enabled = true;
        }
        else
        {
            Debug.LogWarning("Wrong id player");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        SceneManager.LoadScene(1);
    }  
	#endregion

	#region Unity Methods
    void Awake() 
    {
        _normalSize = _playersScore[0].GetComponent<RectTransform>().sizeDelta;
    }

    void Start() 
    {
        _winnerText.enabled = false;
        _pauseText.enabled = true;
    }
	#endregion
}
