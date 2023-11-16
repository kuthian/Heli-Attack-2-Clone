using UnityEngine;
using System;
using System.Collections;
using System.Text;
using Steamworks;

public class SteamCloud : SCPSingleton<SteamCloud> {

	public static bool isAccessible;
	public static string DefaultSaveFileName = "HighScoreSaveFile";

    void Start()
    {
        if (SteamManager.Initialized)
        {
            string username = SteamFriends.GetPersonaName();
            Debug.Log($"Steam Username: {username}");
            if (SteamRemoteStorage.IsCloudEnabledForApp())
            {
                Debug.Log("Steam Cloud is enabled.");
                isAccessible = true;
            } else
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
		int fileSize = SteamRemoteStorage.GetFileSize(DefaultSaveFileName);
		if (fileSize == 0) 
			return 0;

		byte[] data = new byte[fileSize];
		SteamRemoteStorage.FileRead(DefaultSaveFileName, data, fileSize);

		return BitConverter.ToInt32(data, 0);
 	}

	public static void SaveHighScore(int highScore)
	{
		byte[] data = BitConverter.GetBytes(highScore);
		SteamRemoteStorage.FileWrite(DefaultSaveFileName, data, data.Length);
	}

}