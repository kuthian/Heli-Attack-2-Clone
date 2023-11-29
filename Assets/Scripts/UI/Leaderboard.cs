using Steamworks;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public GameObject textPrefab; // Assign a Text prefab with desired formatting.
    public Transform contentPanel; // Assign the Content panel of your Scroll View.

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        // TODO: Use SteamUserStats to display friend's high scores
        //int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
        //for (int i = 0; i < friendCount; i++)
        //{
        //    CSteamID friendSteamId = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
        //    string friendName = SteamFriends.GetFriendPersonaName(friendSteamId);
        //    AddItemToList(friendName);
        //    // You can now use friendSteamId and friendName as needed
        //}
        //AddItemToList("Item 2");
    }

    public void AddItemToList(string itemText)
    {
        GameObject newText = Instantiate(textPrefab, contentPanel);
        newText.GetComponent<TextMeshProUGUI>().text = itemText;
    }
}