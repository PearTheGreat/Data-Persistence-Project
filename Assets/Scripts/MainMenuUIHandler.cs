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
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;

    private string playerName;

    private void Start()
    {
        highScoreText.text = "Current High Score: " + DataManager.INSTANCE.highScore;
        musicSlider.onValueChanged.AddListener(delegate { SetMusicLevel(); });
        inputField.onValueChanged.AddListener(delegate { SetPlayerName(); });
        musicSlider.value = DataManager.INSTANCE.musicVolume;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToSettingsScreen()
    {
        menuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void SwitchToMenuScreen()
    {
        settingsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void SetPlayerName()
    {
        DataManager.INSTANCE.playerName = inputField.text;
    }

    public void SetMusicLevel()
    {
        DataManager.INSTANCE.musicVolume = musicSlider.value;
        DataManager.INSTANCE.audioSource.volume = DataManager.INSTANCE.musicVolume / 100f;
        musicVolumeText.text = DataManager.INSTANCE.musicVolume + "%";
    }

    public void QuitGame()
    {
        DataManager.INSTANCE.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else   
        Application.Quit();
#endif
    }
}