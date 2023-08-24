using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

  [SerializeField] private TextMeshProUGUI scoreCountText;
  public int Score { get; private set; }

  private void Start()
  {
    AkSoundEngine.SetRTPCValue("score", Score);
  } 

  public void Add( int count )
  {
    Score += count;
    scoreCountText.SetText(Score.ToString());
    AkSoundEngine.SetRTPCValue("score", Score);
  }

}