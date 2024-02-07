using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject pfLeaderboardEntry;
    List<GameObject> entries = new List<GameObject>();

    public void SetLeaderboardEntries(List<LeaderboardEntry> leaderboardEntries)
    {
        foreach (GameObject entry in entries)
        {
            GameObject.Destroy(entry);
        }
        entries.Clear();

        foreach (LeaderboardEntry entry in leaderboardEntries)
        {
            GameObject obj = Instantiate(pfLeaderboardEntry, transform);
            obj.GetComponent<LeaderboardEntryUI>().SetLeaderboardEntry(entry);
            entries.Add(obj);
        }
    }
}