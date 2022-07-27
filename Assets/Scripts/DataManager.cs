using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    static public DataManager INSTANCE;
    
    public int highScore;    
    public string playerName;
    public float musicVolume;

    public AudioSource audioSource;

    private void Awake()
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

    private void Update()
    {
    }

    [System.Serializable]
    class SaveDataAndSettings
    {
        public int highScore;
        public float musicVolume;
    }

    public void SaveData()
    {
        SaveDataAndSettings data = new SaveDataAndSettings();
        data.highScore = highScore;
        data.musicVolume = musicVolume;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataAndSettings data = JsonUtility.FromJson<SaveDataAndSettings>(json);

            highScore = data.highScore;
            musicVolume = data.musicVolume;

            //Music Related
            
            audioSource.volume = musicVolume;
        }
    }
}
