using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI positionText;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetLeaderboardEntry(LeaderboardEntry leaderboardEntry)
    {
        positionText.text = leaderboardEntry.position.ToString() + ".";
        nameText.text = leaderboardEntry.name;
        scoreText.text = leaderboardEntry.score.ToString();
    }
}