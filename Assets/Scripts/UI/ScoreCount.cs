using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    public int Score { get; private set; }

    private void Start()
    {
        AkSoundEngine.SetRTPCValue("score", Score);
    }

    public void Add(int count)
    {
        Score += count;
        scoreText.SetText(Score.ToString());
        AkSoundEngine.SetRTPCValue("score", Score);
    }

    public void SetHighScore(int count)
    {
        highScoreText.SetText(count.ToString());
    }

}