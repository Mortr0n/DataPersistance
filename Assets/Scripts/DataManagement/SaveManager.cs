using System;
using System.IO;
using TMPro.Examples;
using UnityEngine;



public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private string savePath => Application.persistentDataPath + "/savefile.json";
    [SerializeField] private SaveData saveData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveData = LoadGame();
    }
    public void SaveGame (SaveData data)
    {
        try
        {
            saveData = data;
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
                saveData = new SaveData();
            }
        }
        else
        {
            Debug.LogWarning("Save file not found. Returning default data.");
            saveData = new SaveData();
        }
        return saveData;
    }

    public void SaveHighScore(int highScore, string highScoreName)
    {
        SaveData data = LoadGame();
        data.HighScore = highScore;
        data.HighScoreName = highScoreName;
        SaveGame(data);
    }

    public SaveData GetCurrentSaveData()
    {
        return saveData;
    }

}
