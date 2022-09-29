using UnityEngine;

public class AudioAssets : MonoBehaviour {
  
  private static AudioAssets _i;

  public static AudioAssets i {
    get {
      if (_i == null) _i = Instantiate(Resources.Load<AudioAssets>("AudioAssets"));
      return _i;
    }
  }

  [SerializeField]
  private AudioClip[] openCrateClips;

  public AudioClip GetOpenCrateClip()
  {
    return openCrateClips[Random.Range(0, openCrateClips.Length)];
  }

  [SerializeField]
  private AudioClip[] rifleShotClips;

  public AudioClip GetRifleShotClips()
  {
    return rifleShotClips[Random.Range(0, rifleShotClips.Length)];
  }

  [SerializeField]
  private AudioClip[] uziShotClips;

  public AudioClip GetUziShotClips()
  {
    return uziShotClips[Random.Range(0, uziShotClips.Length)];
  }

  [SerializeField]
  private AudioClip explosionClip;

  public AudioClip GetExplosionClip()
  {
    return explosionClip;
  }

}