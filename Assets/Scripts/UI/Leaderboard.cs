using Steamworks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI title;
    public LeaderboardUI leaderboardUI;

    public void Start()
    {
        ShowFriends();
    }

    public void ShowFriends()
    {
        title.text = "Leaderboard - Friends";
        var leaderboardEntries = SteamLeaderboard.GetFriendsLeaderboard();
        leaderboardUI.SetLeaderboardEntries(leaderboardEntries);
    }

    public void ShowWorld()
    {
        title.text = "Leaderboard - World";
        var leaderboardEntries = SteamLeaderboard.GetWorldLeaderboard();
        leaderboardUI.SetLeaderboardEntries(leaderboardEntries);
    }
}