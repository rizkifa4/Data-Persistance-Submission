using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public string NameInput;
    public TMP_InputField NameInputField;
    public TextMeshProUGUI BestScoreText;

    private void Start()
    {
        if (DataPersistanceManager.Instance != null)
        {
            BestScoreText.text = $"Best Score: {DataPersistanceManager.Instance.PlayerScore} by {DataPersistanceManager.Instance.PlayerName}";
        }
        else
        {
            BestScoreText.text = "Best Score: 0";
        }
    }

    public void InputName()
    {
        NameInput = NameInputField.text;
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(NameInput))
        {
            NameInput = "Player";
        }

        DataPersistanceManager.Instance.CurrentPlayerName = NameInput;
        SceneManager.LoadScene(1);
    }

    public void DeleteData()
    {
        DataPersistanceManager.Instance.DeleteData();
        DataPersistanceManager.Instance.CurrentPlayerName = null;
        DataPersistanceManager.Instance.PlayerName = null;
        DataPersistanceManager.Instance.PlayerScore = 0;
        BestScoreText.text = "Best Score: 0";
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
