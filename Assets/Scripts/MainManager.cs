using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreAndNameText;
    public GameObject GameOverScreen;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        DisplayBestResult(); // Update best result
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        SetHighScore(m_Points); //Pass current score to SetHighScore() method
        GameOverScreen.SetActive(true);
    }

    public void LoadMenuScene() // Method for loading Menu scene
    {
        SceneManager.LoadScene(0);
    }

    void SetHighScore(int score) // Method, that receives a score and checks if it is higher
    {                            // than our Singleton's highScore. If it is, update highScore
        if (DataManager.INSTANCE != null)
        {
            if (score > DataManager.INSTANCE.highScore)
            {
                DataManager.INSTANCE.highScore = score;
            }
        }
    }

    void DisplayBestResult() // Method for displaying the message and highScore on screen
    {
        if (DataManager.INSTANCE != null)
        {
            BestScoreAndNameText.text = "Best Score : " + DataManager.INSTANCE.playerName
            + " : " + DataManager.INSTANCE.highScore;
        }
    }
}
