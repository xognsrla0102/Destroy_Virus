using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private int stageNum;
    private float health;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            healthText.text = $"체력 게이지 : {health}%";
        }
    }
    private float pain;
    public float Pain
    {
        get => pain;
        set
        {
            pain = value;
            painText.text = $"고통 게이지 : {pain}%";
        }
    }
    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = $"점수 : {score}";
        }
    }

    [SerializeField] private Text healthText;
    [SerializeField] private Text painText;
    [SerializeField] private Text scoreText;
    [SerializeField] private SpawnManager spawnManager;

    private void Start()
    {
        Health = 100;
        Pain = stageNum == 1 ? 10 : 30;

        if (stageNum == 1)
        {
            Score = 0;
        }
        ReadEnemyData(stageNum);
    }

    private void ReadEnemyData(int stageNum)
    {
        spawnManager.ReadEnemyData(
            Resources.Load<TextAsset>($"Stage{stageNum}_EnemyData").text);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Ranking");
    }
}
