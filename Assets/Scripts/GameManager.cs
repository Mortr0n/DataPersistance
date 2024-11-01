using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public TMP_Text highScoreText;
    private int highScore = 0;
    [SerializeField] private TMP_Text nameText;
    private string currentName;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        

    }

    void Start()
    {
        UpdateHighScoreText(highScore);
    }

    private void SetHighScore(int score)
    {
        highScore = score;
    }

    public void UpdateHighScoreText(int score)
    {
        SetHighScore(score);
        highScoreText.text = $"HIGH SCORE: {highScore}";
    }

    public string GetHighScore()
    {
        string scoreText = highScore.ToString();
        return scoreText;
    }

    public void StartGame()
    {
        if (nameText.text != "") 
        {
            string text = nameText.text;
            SetCurrentName(text);
        }
        SceneManager.LoadScene(1);
    }

    public string GetCurrentName()  { return currentName;  }

    public void SetCurrentName(string name) { currentName = name; }


}
