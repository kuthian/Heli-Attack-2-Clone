using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SteamLeaderboard : PersistentSingleton<SteamLeaderboard>
{
    public static int GetCurrentFriendLeaderboardPosition()
    {
        return 2;
    }
    
    public static int GetCurrentWorldLeaderboardPosition()
    {
        return 2;
    }
    
    // Returns the new position on the leaderboard
    public static int UpdateFriendLeaderboardPosition(int score)
    {
        return 1;
    }
    
    // Returns the new position on the leaderboard
    public static int UpdateWorldLeaderboardPosition(int score)
    {
        return 1;
    }

    public static List<LeaderboardEntry> GetFriendsLeaderboard()
    {
        List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
        for (int i = 1; i <= 10; i++)
        {
            LeaderboardEntry entry = new LeaderboardEntry();
            entry.position = i;
            entry.name = "Player " + i.ToString();
            entry.score = 100 - i * 10;
            leaderboardEntries.Add(entry);
        }

        return leaderboardEntries;
    }
    
    public static List<LeaderboardEntry> GetWorldLeaderboard()
    {
        List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
        for (int i = 1; i <= 10; i++)
        {
            LeaderboardEntry entry = new LeaderboardEntry();
            entry.position = i;
            entry.name = "Gamer " + i.ToString();
            entry.score = (10 - i) * 11;
            leaderboardEntries.Add(entry);
        }

        return leaderboardEntries;
    }
}