using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    static public bool Paused => Time.timeScale == 0;

    private GameObject player;
    private EnemyManager enemyManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private DeathScreen deathScreen;

    private float timePlayed;

    override protected void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        player = GameObject.Find("Player");
    }

    public void Start()
    {
        timePlayed = 0;
        player.GetComponent<Health>().OnHealthZero += GameOver;

        if (SteamCloud.isAccessible)
        {
            HUDManager.ScoreCount.SetHighScore(SteamCloud.GetHighScore());
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameMusic.Paused();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameMusic.Gameplay();
    }

    public void StartMenu()
    {
        Instance.deathScreen.Hide();
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    private void _GameOver()
    {
        if (SteamCloud.isAccessible)
        {
            int previousHighScore = SteamCloud.GetHighScore();
            Debug.Log($"High Score: {previousHighScore}");

            if (HUDManager.ScoreCount.Score > previousHighScore)
            {
                SteamCloud.SaveHighScore(HUDManager.ScoreCount.Score);
                Debug.Log($"New High Score: {HUDManager.ScoreCount.Score}");
            }
        }

        deathScreen.Show();

        deathScreen.ScoreText.SetText(HUDManager.ScoreCount.Score.ToString());
        deathScreen.TimePlayedText.SetText(timePlayed.ToString("F0") + "s");
        deathScreen.AccuracyText.SetText(StatsManager.AccuracyPercentage().ToString("F2") + "%");

        player.GetComponent<PlayerAnimator>().StartDeathSequence();
        player.GetComponent<PlayerController>().BlockInput = true;
        player.GetComponentInChildren<InventoryController>().HideWeapon();
        enemyManager.End();
        HUDManager.HideHUD();
    }

    public static void GameOver()
    {
        Instance._GameOver();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (!Paused)
        {
            timePlayed += Time.deltaTime;
        }

        if (Input.GetKeyDown("escape"))
        {
            if (!Paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

}