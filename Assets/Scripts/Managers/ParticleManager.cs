using UnityEngine;

public class ParticleManager : Singleton<ParticleManager> {

  public static void PlayExplodedEffect( Transform parent )
  {
    __StartEffect( ParticleAssets.i.ExplodedEffect, parent, 5.0f );
  }

  public static void PlayDamagedPlayerEffect( Transform parent )
  {
    __StartEffect( ParticleAssets.i.DamagedPlayerEffect, parent, 1.0f );
  }

  private static void __StartEffect( ParticleSystem effect, Transform parent, float lifetimeSeconds )
  {
    ParticleSystem particle = Instantiate(effect, parent);
    Destroy(particle.gameObject, lifetimeSeconds);
  }

};