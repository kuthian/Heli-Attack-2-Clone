using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : Singleton<GameManager> {

  private GameObject _player;
  private EnemyManager _enemyManager;
  [SerializeField]
  private GameObject _pauseMenuUI;
  [SerializeField]
  private DeathScreen _deathScreen;

  private float timePlayed;

  static public bool Paused => Time.timeScale == 0;

  public void Start()
  {
    timePlayed = 0;
    _player = GameObject.Find("Player");
    _enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    _player.GetComponent<PlayerController>().AddWeapon( 
        ItemManager.GetRiflePrefab()
     );
    _player.GetComponent<Health>().OnHealthZero += GameOver;
  }

  public void PauseGame()
  {
    _pauseMenuUI.SetActive(true);
    Time.timeScale = 0;
    GameMusic.Paused();
  }

  public void ResumeGame()
  {
    _pauseMenuUI.SetActive(false);
    Time.timeScale = 1;
    GameMusic.Gameplay();
  }

  public void StartMenu()
  {
    Instance._deathScreen.Hide();
    SceneManager.LoadScene("StartMenu");
    Time.timeScale = 1;
  }

  private void _GameOver()
  {
    if (SteamCloud.isAccessible)
    {
        int previousHighScore = SteamCloud.GetHighScore();
        Debug.Log($"High Score: {previousHighScore}");

        if (HUDManager.ScoreCount.Score > previousHighScore) {
          SteamCloud.SaveHighScore(HUDManager.ScoreCount.Score);
          Debug.Log($"New High Score: {HUDManager.ScoreCount.Score}");
        }
    }

    _deathScreen.Show();

    _deathScreen.ScoreText.SetText( HUDManager.ScoreCount.Score.ToString() );
    _deathScreen.TimePlayedText.SetText( timePlayed.ToString("F0") + "s" );
    _deathScreen.AccuracyText.SetText( StatsManager.AccuracyPercentage().ToString("F2") + "%" );

    _player.GetComponent<PlayerAnimator>().StartDeathSequence();
    _player.GetComponent<PlayerController>().BlockInput = true;
    _player.GetComponent<PlayerController>().HideWeapon();
    _enemyManager.End();
    // Time.timeScale = 0.1f;
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
    if (!Paused) {
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