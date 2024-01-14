using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        GameObject.Find("MainMenu").GetComponent<MainMenu>().LoadMainScene();
        //SceneManager.LoadScene("MainScene");
    }
}