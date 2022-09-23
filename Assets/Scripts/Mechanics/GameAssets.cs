using UnityEngine;

public class GameAssets : MonoBehaviour {
  
  private static GameAssets _i;

  public static GameAssets i {
    get {
      if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
      return _i;
    }
  }

  public AudioClip[] openCrateClips;

  public AudioClip GetOpenCrateClip()
  {
    return openCrateClips[Random.Range(0, openCrateClips.Length)];
  }

  public AudioClip[] rifleShotClips;

  public AudioClip GetRifleShotClips()
  {
    return rifleShotClips[Random.Range(0, rifleShotClips.Length)];
  }

  public AudioClip[] uziShotClips;

  public AudioClip GetUziShotClips()
  {
    return uziShotClips[Random.Range(0, uziShotClips.Length)];
  }
}