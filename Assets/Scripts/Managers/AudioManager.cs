using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

  public static void PlaySound( AudioClip clip, float volume = 1.0f )
  {
    __PlaySound( clip, volume );
  }

  public static void PlaySoundDelayed( AudioClip clip, float seconds, float volume = 1.0f )
  {
    __PlaySound( clip, volume, seconds );
  }

  private static void __PlaySound( AudioClip clip, float volume = 1.0f, float delaySeconds = 0 )
  {
    if ( !clip ) return;

    GameObject sound = new GameObject("Sound");
    AudioSource source = sound.AddComponent<AudioSource>();
    source.clip = clip;
    source.volume = volume;

    DynamicObjects.Reparent(sound, DynamicObjects.Sounds);

    if ( delaySeconds == 0 ) source.Play();
    else source.PlayDelayed(delaySeconds);

    float clipDurationSeconds = clip.samples / clip.frequency;
    GameObject.Destroy(source.gameObject, delaySeconds + clipDurationSeconds + 2f);
  }

};