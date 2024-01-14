using UnityEngine;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeOutCanvas;
    public PlayableDirector launchArcadeModeTimeline;
    internal ImageFaderGroup mainMenuFader;

    private void Start()
    {
        launchArcadeModeTimeline = GameObject.Find("LaunchArcadeMode").GetComponent<PlayableDirector>();
    }

    public void StartArcadeMode()
    {
        launchArcadeModeTimeline.Play();
    }

    public void StartTransitionToMainScene()
    {
    }

    public void LoadMainScene()
    {
    }

    public void OpenLeaderBoard()
    {
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}