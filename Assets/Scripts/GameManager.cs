using UnityEngine;
using UnityEngine.UI;

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
            healthText.text = $"ü�� ������ : {health}%";
        }
    }
    private float pain;
    public float Pain
    {
        get => pain;
        set
        {
            pain = value;
            painText.text = $"���� ������ : {pain}%";
        }
    }
    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = $"���� : {score}";
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

    private void Update()
    {
        
    }

    public void GameOver()
    {

    }
}
