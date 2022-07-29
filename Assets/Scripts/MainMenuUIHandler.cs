using UnityEngine;
using UnityEngine.SceneManagement; // Used for loading scenes
using UnityEngine.UI; // Used for interacting with UI elements
using TMPro; // Used for interacting with TextMeshPro UI elements
#if UNITY_EDITOR
using UnityEditor; // Used for testing some functionalities while inside Unity Editor
#endif

public class MainMenuUIHandler : MonoBehaviour // Class for handling UI elements in Menu
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button clearDataButton;

    private void Start()
    {
        highScoreText.text = "Current High Score: " + DataManager.INSTANCE.highScore;
        musicSlider.value = DataManager.INSTANCE.musicVolume; // Putting slider in correct position
        DataManager.INSTANCE.audioSource.volume = DataManager.INSTANCE.musicVolume / 100f;
        musicVolumeText.text = DataManager.INSTANCE.musicVolume + "%";
        inputField.text = DataManager.INSTANCE.playerName; // Pre-writing player name in the box
        musicSlider.onValueChanged.AddListener(delegate { SetMusicLevel(); });
        inputField.onValueChanged.AddListener(delegate { SetPlayerName(); });
        clearDataButton.onClick.AddListener(delegate { DataManager.INSTANCE.ClearPlayerData(); });
    } // ^ Same thing as assigning the OnClick() actions in Editor, just from code (as I understand)

    public void LoadMainScene() // Method loading game scene
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToSettingsScreen() // Method opening settings and closing menu
    {
        menuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void SwitchToMenuScreen() // Method closing settings and opening menu
    {
        settingsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void SetPlayerName() // Method for updating playerName variable with content of
    {                           // inputField text (used as a listener on line 25)
        DataManager.INSTANCE.playerName = inputField.text;
    }

    public void SetMusicLevel() // Method for setting musicVolume based on slider's value,
    {                           // setting audio volume, and showing a volume % to the user.
                                // (we divide volume by 100, because musicVolume varies 
                                // from 0 to 100, but audioSource.volume takes values from 0.0 to 1.0)
        DataManager.INSTANCE.musicVolume = musicSlider.value;
        DataManager.INSTANCE.audioSource.volume = DataManager.INSTANCE.musicVolume / 100f;
        musicVolumeText.text = DataManager.INSTANCE.musicVolume + "%";
    }

    public void QuitGame() // Method for quitting the application, and saving player data
    {                      // when doing so
        DataManager.INSTANCE.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else   
        Application.Quit();
#endif
    }
}