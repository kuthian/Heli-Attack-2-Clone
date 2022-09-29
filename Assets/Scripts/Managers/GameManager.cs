using UnityEngine;

public class GameManager : Singleton<GameManager> {

  static public bool Paused => Time.timeScale == 0;

  private static void PauseGame()
  {
    Time.timeScale = 0;
  }

  private static void ResumeGame()
  {
    Time.timeScale = 1;
  }

  public void Update()
  {
    if (Input.GetKeyDown("p"))
    {
      if (!Paused) PauseGame();
      else ResumeGame();
    }
  }

}