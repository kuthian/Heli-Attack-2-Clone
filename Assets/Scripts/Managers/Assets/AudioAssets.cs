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
  private AudioClip[] _openCrateClips;

  public AudioClip GetOpenCrateClip()
  {
    return Utils.RandomInRange( _openCrateClips );
  }

  [SerializeField]
  private AudioClip[] _rifleShotClips;

  public AudioClip GetRifleShotClip()
  {
    return Utils.RandomInRange( _rifleShotClips );
  }

  [SerializeField]
  private AudioClip[] _uziShotClips;

  public AudioClip GetUziShotClip()
  {
    return Utils.RandomInRange( _uziShotClips );
  }

  [SerializeField]
  private AudioClip[] _shotgunClips;

  public AudioClip GetShotgunShotClip()
  {
    return Utils.RandomInRange( _shotgunClips );
  }

  [SerializeField]
  private AudioClip _explosionClip;

  public AudioClip GetExplosionClip()
  {
    return _explosionClip;
  }

}