using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SaveManager saveManager;
    [SerializeField] public TMP_Text highScoreText;
    private int highScore = 0;
    [SerializeField] private TMP_Text nameText;
    private string currentName;
    private SaveData saveData;
    
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
        saveManager = SaveManager.Instance;
        if (saveManager == null)
        {
            Debug.LogError("SaveManager Instance not found!");
            return;
        }

            if (saveData != null)
        {
            UpdateHighScoreText(saveData.HighScore);
        }
        
    }

    //public SaveData GetSaveDataFromGameManager() { return saveData; }

    private void SetHighScore(int score)
    {
        highScore = score;
        
    }

    public void UpdateHighScoreText(int score)
    {
        SetHighScore(score);
        highScoreText.text = $"HIGH SCORE: {highScore}";
    }

    public int GetHighScore()
    {
        //string scoreText = highScore.ToString();
        return highScore;
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
