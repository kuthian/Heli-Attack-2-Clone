using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

  private GameObject _player;
  [SerializeField]
  private GameObject _pauseMenuUI;

  public void Start()
  {
    _player = GameObject.Find("Player");
    _player.GetComponent<PlayerController>().AddWeapon( 
        ItemManager.GetRiflePrefab()
     );
  }  

  static public bool Paused => Time.timeScale == 0;

  public void PauseGame()
  {
    _pauseMenuUI.SetActive(true);
    Time.timeScale = 0;
    AkSoundEngine.SetState("gameplay_state", "paused");
  }

  public void ResumeGame()
  {
    _pauseMenuUI.SetActive(false);
    Time.timeScale = 1;
    AkSoundEngine.SetState("gameplay_state", "gameplay");
  }

  public void StartMenu()
  {
    SceneManager.LoadScene("StartMenu");
    Time.timeScale = 1;
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