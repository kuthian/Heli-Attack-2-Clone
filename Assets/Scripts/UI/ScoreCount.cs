using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

  [SerializeField] private TextMeshProUGUI scoreCountText;
  private int scoreCount = 0;

  public void Add( int count )
  {
    scoreCount += count;
    scoreCountText.SetText(scoreCount.ToString());
    AkSoundEngine.SetRTPCValue("score", scoreCount);
  }

}