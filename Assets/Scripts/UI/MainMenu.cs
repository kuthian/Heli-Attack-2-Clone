using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void OpenLeaderBoard()
    {
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}