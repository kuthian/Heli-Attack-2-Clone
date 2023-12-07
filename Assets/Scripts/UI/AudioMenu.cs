using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public TextMeshProUGUI masterVolumeValue;

    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumeValue;

    public Slider sfxVolumeSlider;
    public TextMeshProUGUI sfxVolumeValue;

    private VolumeSettings volumeSettings;

    void Start()
    {
        volumeSettings = SteamCloud.GetVolumeSettings();

        UpdateValues();
        UpdateSliders();

        masterVolumeSlider.onValueChanged.AddListener(delegate { updateMasterVolume(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { updateMusicVolume(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { updateSfxVolume(); });
    }

    private void updateMasterVolume()
    {
        volumeSettings.Master = (int)masterVolumeSlider.value;
        SaveVolumeSettings();
        UpdateValues();
    }
    
    private void updateMusicVolume()
    {
        volumeSettings.Music = (int)musicVolumeSlider.value;
        SaveVolumeSettings();
        UpdateValues();
    }    

    private void updateSfxVolume()
    {
        volumeSettings.Sfx = (int)sfxVolumeSlider.value;
        SaveVolumeSettings();
        UpdateValues();
    }
    
    private void UpdateValues()
    {
        masterVolumeValue.text = volumeSettings.Master.ToString();
        musicVolumeValue.text = volumeSettings.Music.ToString();
        sfxVolumeValue.text = volumeSettings.Sfx.ToString();
    }    
    private void UpdateSliders()
    {
        masterVolumeSlider.value = volumeSettings.Master;
        musicVolumeSlider.value = volumeSettings.Music;
        sfxVolumeSlider.value = volumeSettings.Sfx;
    }

    public void SaveVolumeSettings()
    {
        SteamCloud.SaveVolumeSettings(volumeSettings);
    }
}