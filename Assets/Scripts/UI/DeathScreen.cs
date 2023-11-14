using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour {

  public TextMeshProUGUI ScoreText;

  public TextMeshProUGUI TimePlayedText;

  public TextMeshProUGUI AccuracyText;

  public void Show()
  {
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }


}