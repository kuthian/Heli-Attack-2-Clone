using UnityEngine;

public class ParticleManager : Singleton<ParticleManager> {

  public static void PlayExplodedEffect( Transform parent )
  {
    _StartEffect( ParticleAssets.i.ExplodedEffect, parent, 5.0f );
  }

  private static void _StartEffect( ParticleSystem effect, Transform parent, float lifetimeSeconds )
  {
    ParticleSystem particle = Instantiate(effect, parent);
    Destroy(particle.gameObject, lifetimeSeconds);
  }

};