using UnityEngine;
using System;
using System.Collections;
using System.Text;
using Steamworks;

public class SteamCloud : Singleton<SteamCloud>
{
    public static bool isAccessible;
    public static string HighscoreSaveFileName = "HighScoreSaveFile";
    public static string VolumeSettingsSaveFileName = "VolumeSettingsSaveFile";

    override protected void Awake()
    {
        if (SteamManager.Initialized)
        {
            string username = SteamFriends.GetPersonaName();
            Debug.Log($"Steam Username: {username}");
            if (SteamRemoteStorage.IsCloudEnabledForApp())
            {
                Debug.Log("Steam Cloud is enabled.");
                isAccessible = true;
            }
            else
            {
                Debug.Log("Steam Cloud is not enabled.");
                isAccessible = false;
            }
        }
        else
        {
            Debug.Log("Steam API is not initialized.");
        }
    }

    public static int GetHighScore()
    {
        int fileSize = SteamRemoteStorage.GetFileSize(HighscoreSaveFileName);
        if (fileSize == 0)
            return 0;

        byte[] data = new byte[fileSize];
        SteamRemoteStorage.FileRead(HighscoreSaveFileName, data, fileSize);

        return BitConverter.ToInt32(data, 0);
    }

    public static void SaveHighScore(int highScore)
    {
        byte[] data = BitConverter.GetBytes(highScore);
        SteamRemoteStorage.FileWrite(HighscoreSaveFileName, data, data.Length);
    }

    public static VolumeSettings GetVolumeSettings()
    {
        int fileSize = SteamRemoteStorage.GetFileSize(VolumeSettingsSaveFileName);
        if (fileSize == 0)
            return new VolumeSettings();

        byte[] data = new byte[fileSize];
        SteamRemoteStorage.FileRead(VolumeSettingsSaveFileName, data, fileSize);

        return VolumeSettings.FromByteArray(data);
    }

    public static void SaveVolumeSettings(VolumeSettings volumeSettings)
    {
        byte[] data = volumeSettings.ToByteArray();
        SteamRemoteStorage.FileWrite(VolumeSettingsSaveFileName, data, data.Length);
    }
}