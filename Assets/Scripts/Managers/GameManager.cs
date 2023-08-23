using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

  private GameObject _player;
  private EnemyManager _enemyManager;
  [SerializeField]
  private GameObject _pauseMenuUI;
  [SerializeField]
  private GameObject _deathScreenUI;

  public void Start()
  {
    _player = GameObject.Find("Player");
    _enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    _player.GetComponent<PlayerController>().AddWeapon( 
        ItemManager.GetRiflePrefab()
     );
    _player.GetComponent<Health>().OnHealthZero += GameOver;
  }

  static public bool Paused => Time.timeScale == 0;

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
    _deathScreenUI.SetActive(false);
    SceneManager.LoadScene("StartMenu");
    Time.timeScale = 1;
  }

  public static void GameOver()
  {
    Time.timeScale = 0.1f;
    Instance._deathScreenUI.SetActive(true);
    Instance._player.GetComponent<PlayerController>().BlockInput = true;
    Instance._enemyManager.End();
    HUDManager.HideHUD();
  }

  public void Quit()
  {
    Application.Quit();
  }

  public void Update()
  {
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