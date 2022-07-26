using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager INSTANCE;
    
    public int highScore;    
    public string playerName;

    private MainMenuUIHandler uiHandler;

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
        uiHandler = GameObject.Find("MainMenuUIHandler").GetComponent<MainMenuUIHandler>();
        print(highScore);
    }

    private void Update()
    {
        if (uiHandler != null)
        {
            playerName = uiHandler.GetPlayerName();
        }
    }
}
