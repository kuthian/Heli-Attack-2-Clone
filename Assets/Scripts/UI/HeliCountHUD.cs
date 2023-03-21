using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeliCountHUD : MonoBehaviour {

  [SerializeField] private TextMeshProUGUI heliCountText;
  private int heliCount = 0;

  public void Add( int count )
  {
    heliCount += count;
    heliCountText.SetText("" + heliCount);
    if(heliCount >= 5)
        {
            AkSoundEngine.SetRTPCValue("score", 5);
        }
  }



    

}