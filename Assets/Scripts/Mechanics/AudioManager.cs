using UnityEngine;

public static class AudioManager {

  public static void PlaySound( AudioClip clip )
  {
    GameObject sound = new GameObject("Sound");
    AudioSource source = sound.AddComponent<AudioSource>();
    source.PlayOneShot(clip);
    GameObject.Destroy(source.gameObject, (clip.samples / clip.frequency) + 0.5f);
  }

  public static void PlaySoundDelayed( AudioClip clip, float seconds )
  {
    GameObject sound = new GameObject("Sound");
    AudioSource source = sound.AddComponent<AudioSource>();
    source.clip = clip;
    source.PlayDelayed(seconds);
    GameObject.Destroy(source.gameObject, seconds + (clip.samples / clip.frequency) + 0.5f);
  }

  public static void PlayCrateOpen()
  {
    PlaySound(GameAssets.i.GetOpenCrateClip());
  }

  public static void PlayShootingSound( GunType type )
  {
    switch (type) {
      case GunType.rifle:
        PlaySound(GameAssets.i.GetRifleShotClips());
        break;
      case GunType.uzi:
        PlaySound(GameAssets.i.GetUziShotClips());
        PlaySoundDelayed(GameAssets.i.GetUziShotClips(), 0.2f);
        break;
      default:
        break;
    }
  }
}