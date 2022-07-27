// Hello to whoever is reading this! I tried to write my thoughts and understanding of things
// in the comments, so it might be useful for someone who doesn't understand something. This
// code is surely messy and probably not the most efficient, please don't judge too much, I'm
// learning. Oh, and I don't know how to link PlayerName with HighScore, so I don't have the
// feature to show individual player's score.

using UnityEngine;
using System.IO; // Used for writing and reading information from external files (JSON)

public class DataManager : MonoBehaviour // Singleton class that's gonna contain player data   
{                                        // and save it in JSON file
    static public DataManager INSTANCE;
    
    public int highScore;      // In my project it contains player's high score, player name,
    public string playerName;  // and preffered music volume (from the settings menu)
    public float musicVolume;

    public AudioSource audioSource; // It also has an Audio Source so the music keeps playing
                                    // between scenes
    private void Awake() // Singleton initialization
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LoadData();
    }

    [System.Serializable]      // Creating Serializable class inside our Singleton, so it                      
    class SaveDataAndSettings  // can pass the data in and out of external JSON file
    {
        public int highScore;
        public float musicVolume;
        public string playerName;
    }

    public void SaveData() // Method for getting current data and writing it to /savefile.json
    {
        SaveDataAndSettings data = new SaveDataAndSettings();
        data.highScore = highScore;
        data.musicVolume = musicVolume;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData() // Method for getting data from /savefile.json and assigning it
    {                       
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataAndSettings data = JsonUtility.FromJson<SaveDataAndSettings>(json);

            highScore = data.highScore;
            musicVolume = data.musicVolume;
            playerName = data.playerName;
        }
    }

    public void ClearPlayerData() // Method that manually sets highscore and playername to null
    {                             // (mainly for testing reasons)
        SaveDataAndSettings data = new SaveDataAndSettings();
        data.highScore = 0;
        data.musicVolume = musicVolume;
        data.playerName = "";

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        LoadData();
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
