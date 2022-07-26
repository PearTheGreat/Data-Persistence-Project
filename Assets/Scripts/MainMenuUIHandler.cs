using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private string playerName;

    private void Start()
    {
        highScoreText.text = "Current High Score: " + DataManager.INSTANCE.highScore;    
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public string GetPlayerName()
    {
        playerName = inputField.text;
        return playerName;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else   
        Application.Quit();
#endif
    }
}