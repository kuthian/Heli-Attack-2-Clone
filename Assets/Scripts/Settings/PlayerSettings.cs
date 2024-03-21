using System;
using UnityEngine;

public class PlayerSettings : PersistentSingleton<PlayerSettings>
{
    public VolumeSettings Volume;

    private void Awake()
    {
        Volume = VolumeSettings.Load();
        Volume.Apply();
    }

    public static void Save()
    {
        Instance.Volume.Save();
        PlayerPrefs.Save();
    }

    public static ref VolumeSettings GetVolume()
    {
        return ref Instance.Volume;
    }
}