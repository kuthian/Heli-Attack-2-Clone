using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

  public static void PlaySound( AudioClip clip, float volume = 1.0f )
  {
    _PlaySound( clip, volume );
  }

  public static void PlaySoundDelayed( AudioClip clip, float seconds, float volume = 1.0f )
  {
    _PlaySound( clip, volume, seconds );
  }

  public static void PlayCrateOpen()
  {
    PlaySound(AudioAssets.i.GetOpenCrateClip());
  }

  public static void PlayExplosion()
  {
    PlaySound(AudioAssets.i.GetExplosionClip(), 0.25f);
  }

  public static void PlayShootingSound( GunType type )
  {
    switch (type) {
      case GunType.rifle:
        PlaySound(AudioAssets.i.GetRifleShotClip());
        break;
      case GunType.uzi:
        PlaySound(AudioAssets.i.GetUziShotClip());
        PlaySoundDelayed(AudioAssets.i.GetUziShotClip(), 0.2f);
        break;
      case GunType.shotgun:
        PlaySound(AudioAssets.i.GetShotgunShotClip());
        break;
      default:
        break;
    }
  }

  private static void _PlaySound( AudioClip clip, float volume = 1.0f, float delaySeconds = 0 )
  {
    GameObject sound = new GameObject("Sound");
    AudioSource source = sound.AddComponent<AudioSource>();
    source.clip = clip;
    source.volume = volume;

    DynamicObjects.AssignChild( sound );

    if ( delaySeconds == 0 ) source.Play();
    else source.PlayDelayed(delaySeconds);

    float clipDurationSeconds = clip.samples / clip.frequency;
    GameObject.Destroy(source.gameObject, delaySeconds + clipDurationSeconds + 0.5f);
  }

};