using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

  [SerializeField] private TextMeshProUGUI scoreCountText;
  private int scoreCount = 0;

  private void Start()
  {
    AkSoundEngine.SetRTPCValue("score", scoreCount);
  } 

  public void Add( int count )
  {
    scoreCount += count;
    scoreCountText.SetText(scoreCount.ToString());
    AkSoundEngine.SetRTPCValue("score", scoreCount);
  }

}