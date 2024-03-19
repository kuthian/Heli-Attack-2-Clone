using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI BannerText;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI TimePlayedText;

    public TextMeshProUGUI AccuracyText;

    public RankBadge FriendBadge;

    public RankBadge WorldBadge;

    public void Start()
    {
        FriendBadge.SetRank(SteamLeaderboard.GetCurrentFriendLeaderboardPosition());
        WorldBadge.SetRank(SteamLeaderboard.GetCurrentWorldLeaderboardPosition());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


}