using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject AudioMenu;
    public GameObject ExitConfirmation;
    public GameObject MainMenuConfirmation;

    void OnEnable()
    {
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        ExitConfirmation.SetActive(false);
        MainMenuConfirmation.SetActive(false);
    }
}