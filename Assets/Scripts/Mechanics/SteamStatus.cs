using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamStatus : MonoBehaviour {

	void Start() {
		if(SteamManager.Initialized) {
			string username = SteamFriends.GetPersonaName();
			Debug.Log($"Steam Username: {username}");
		}

		if (SteamRemoteStorage.IsCloudEnabledForApp())
		{
			Debug.Log("Steam Cloud is available.");
		}
	}

}