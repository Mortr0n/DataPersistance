using System;
using System.IO;
using TMPro.Examples;
using UnityEngine;



public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private string savePath => Application.persistentDataPath + "/savefile.json";
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

        LoadGame();
    }
    public void SaveGame (SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, json);
            Debug.Log("Game saved Successfully");
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }
    }

    public SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                Debug.Log("Game loaded successfully");
                return data;
            }
            catch (IOException e)
            {
                Debug.LogError($"Failed to load game: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning("Save file not found. Returning default data.");
        }
        return new SaveData();
    }

    public void SaveHighScore(int highScore, string highScoreName)
    {
        SaveData data = LoadGame();
        data.HighScore = GameManager.Instance.GetHighScore();
        data.HighScoreName = GameManager.Instance.GetCurrentName();
        SaveGame(data);
    }

    public SaveData GetCurrentSaveData()
    {
        return saveData;
    }

}
