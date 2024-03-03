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
        List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
        for (int i = 1; i <= 10; i++)
        {
            LeaderboardEntry entry = new LeaderboardEntry();
            entry.position = i;
            entry.name = "Player " + i.ToString();
            entry.score = 100 - i * 10;
            leaderboardEntries.Add(entry);
        }

        leaderboardUI.SetLeaderboardEntries(leaderboardEntries);
    }

    public void ShowWorld()
    {
        title.text = "Leaderboard - World";
        List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
        for (int i = 1; i <= 10; i++)
        {
            LeaderboardEntry entry = new LeaderboardEntry();
            entry.position = i;
            entry.name = "Gamer " + i.ToString();
            entry.score = (10 - i) * 11;
            leaderboardEntries.Add(entry);
        }
        leaderboardUI.SetLeaderboardEntries(leaderboardEntries);
    }
}