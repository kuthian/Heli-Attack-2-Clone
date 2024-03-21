using UnityEngine;

public class VolumeSettings
{
    public static string MasterKey = "MasterVolume";
    public static string MusicKey = "MusicVolume";
    public static string SfxKey = "SfxVolume";

    public int Master = 100;
    public int Music = 100;
    public int Sfx = 100;

    public void Apply()
    {
    }

    public void Save()
    {
        PlayerPrefs.SetInt(MasterKey, Master);
        PlayerPrefs.SetInt(MusicKey, Music);
        PlayerPrefs.SetInt(SfxKey, Sfx);
    }

    public static VolumeSettings Load()
    {
        VolumeSettings volumeSettings = new VolumeSettings();
        volumeSettings.Master = PlayerPrefs.HasKey(MasterKey) ? PlayerPrefs.GetInt(MasterKey) : 100;
        volumeSettings.Music = PlayerPrefs.HasKey(MusicKey) ? PlayerPrefs.GetInt(MusicKey) : 100;
        volumeSettings.Sfx = PlayerPrefs.HasKey(SfxKey) ? PlayerPrefs.GetInt(SfxKey) : 100;
        return volumeSettings;
    }
}
