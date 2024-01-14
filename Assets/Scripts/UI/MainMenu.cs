using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeOutCanvas;
    private Animator bemmySilhouetteAnimator;
    internal ImageFaderGroup mainMenuFader;

    private void Start()
    {
        mainMenuFader = GetComponent<ImageFaderGroup>();
        bemmySilhouetteAnimator = GameObject.Find("Bemmy").GetComponent<Animator>();
    }

    public float waitTime1 = 1.5f;
    public float waitTime2 = 0.5f;

    public IEnumerator _StartTransitionToMainScene()
    {
        mainMenuFader.FadeOut();
        fadeOutCanvas.SetActive(true);
        yield return new WaitForSeconds(waitTime1);
        bemmySilhouetteAnimator.SetTrigger("Jump");
        yield return new WaitForSeconds(waitTime2);
        fadeOutCanvas.GetComponent<ImageFader>().FadeIn();
    }

    public void StartTransitionToMainScene()
    {
        StartCoroutine(_StartTransitionToMainScene());
    }

    public void LoadMainScene()
    {
        Debug.Log("LoadMainScene");
        //mainMenuFader.FadeIn();
        //fadeOutCanvas.GetComponent<UnityEngine.UI.Image>().GetComponent<CanvasRenderer>().SetAlpha(0f); ;
        //fadeOutCanvas.SetActive(false);
            //SceneManager.LoadScene("MainScene");
    }

    public void OpenLeaderBoard()
    {
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}