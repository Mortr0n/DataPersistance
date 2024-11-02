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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        saveData = SaveManager.Instance.GetCurrentSaveData();
        
        FindUIElements();
        if (saveData != null)
        {
            UpdateHighScoreText(saveData.HighScore);
        }
    }

    private void FindUIElements()
    {
        highScoreText = GameObject.Find("HighScoreText")?.GetComponent<TMP_Text>();
        nameText = GameObject.Find("NameText")?.GetComponent <TMP_Text>();
    }

    void Start()
    { 
        saveManager = SaveManager.Instance;
        if (saveManager == null)
        {
            Debug.LogError("SaveManager Instance not found!");
            return;
        }
        saveData = saveManager.GetCurrentSaveData();
        //Debug.Log($"score {saveManager.GetCurrentSaveData().HighScore} "); 
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
        highScoreText.text = $"HIGH SCORE = {saveData.HighScoreName} {highScore}";
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
