using UnityEngine;

public class AudioAssets : MonoBehaviour {
  
  private static AudioAssets _i;

  public static AudioAssets i {
    get {
      if (_i == null) _i = Instantiate(Resources.Load<AudioAssets>("AudioAssets"));
      return _i;
    }
  }

  private AudioClip RandomInRange( AudioClip[] clips ) {
    return clips[Random.Range(0, clips.Length)];
  }

  [SerializeField]
  private AudioClip[] openCrateClips;

  public AudioClip GetOpenCrateClip()
  {
    return RandomInRange( openCrateClips );
  }

  [SerializeField]
  private AudioClip[] rifleShotClips;

  public AudioClip GetRifleShotClip()
  {
    return RandomInRange( rifleShotClips );
  }

  [SerializeField]
  private AudioClip[] uziShotClips;

  public AudioClip GetUziShotClip()
  {
    return RandomInRange( uziShotClips );
  }

  [SerializeField]
  private AudioClip[] shotgunClips;

  public AudioClip GetShotgunShotClip()
  {
    return RandomInRange( shotgunClips );
  }

  [SerializeField]
  private AudioClip explosionClip;

  public AudioClip GetExplosionClip()
  {
    return explosionClip;
  }

}