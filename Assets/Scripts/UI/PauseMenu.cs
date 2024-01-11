using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject AudioMenu;

    void OnEnable()
    {
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
    }
}