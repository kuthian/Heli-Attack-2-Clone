using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    public void OnEnable()
    {
        SceneManager.LoadScene("MainScene");
    }
}