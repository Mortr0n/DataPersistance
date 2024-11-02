using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public TMP_Text mainHighScoreText;
    public SaveManager saveManager;
    
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    //SaveData m_SaveData;


    // Start is called before the first frame update
    void Start()
    {
        //m_SaveData = SaveManager.Instance.GetCurrentSaveData();
        saveManager = SaveManager.Instance;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        string hScoreName = saveManager.GetCurrentSaveData().HighScoreName;
        if (hScoreName != "")
        {

            string hScore = (GameManager.Instance.GetHighScore()).ToString();
            
            mainHighScoreText.text = $"Best Score: {hScoreName}: {hScore} ";
         }

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        Debug.Log("Game Over called");
        SaveData currSaveData = SaveManager.Instance.GetCurrentSaveData();
        string currName;
        // instead of having to delete or remove the file, just rewritethe data to 0
        // currName = GameManager.Instance.GetCurrentName();
        // saveManager.SaveHighScore(0, currName); 
        if (currSaveData != null)
        {
            if (m_Points > SaveManager.Instance.GetCurrentSaveData().HighScore)
            {
                GameManager.Instance.UpdateHighScoreText(m_Points);
                currName = GameManager.Instance.GetCurrentName();
                SaveManager.Instance.SaveHighScore(m_Points, currName);
            }
        }
        else
        {
            Debug.LogError("currentSaveData is null in GameOver()");
        }
       
        FinalizeGameOver();
    }

    private void FinalizeGameOver()
    {
        Debug.Log("Finalize GameOver Called");
        m_GameOver = true;
        if (GameOverText != null)
        {
            GameOverText.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverText is not assigned!");
        }
        StartCoroutine(GameOverMenuWait());
    }

    private IEnumerator GameOverMenuWait()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);

    }
}
