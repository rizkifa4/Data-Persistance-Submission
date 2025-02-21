using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    private string saveFilePath;

    private string _currentPlayerName;
    public string CurrentPlayerName
    {
        get => _currentPlayerName;
        set => _currentPlayerName = value;
    }

    private string _playerName;
    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }

    private int _playerScore;
    public int PlayerScore
    {
        get => _playerScore;
        set => _playerScore = value;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Application.persistentDataPath + "/savefile.json";
        LoadData();
    }

    public void SaveData()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.PlayerScore = PlayerScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            if (!string.IsNullOrEmpty(json))
            {
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                if (data != null)
                {
                    PlayerName = data.PlayerName;
                    PlayerScore = data.PlayerScore;
                }
            }
        }
        else
        {
            // Default values
            PlayerName = "Budi Oey";
            PlayerScore = 5;
        }
    }

    public void SetHighScore(string playerName, int score)
    {
        PlayerName = playerName;
        PlayerScore = score;
        SaveData();
    }

    public bool HasFileExists()
    {
        return File.Exists(saveFilePath);
    }

    public void DeleteData()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found!");
            return;
        }

        File.Delete(saveFilePath);
    }
}

[System.Serializable]
class SaveData
{
    public string PlayerName;
    public int PlayerScore;
}
