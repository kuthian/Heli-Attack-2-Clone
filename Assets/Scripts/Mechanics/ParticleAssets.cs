using UnityEngine;

public class ParticleAssets : MonoBehaviour {
  
  private static ParticleAssets _i;

  public static ParticleAssets i {
    get {
      if (_i == null) _i = Instantiate(Resources.Load<ParticleAssets>("ParticleAssets"));
      return _i;
    }
  }

  [SerializeField]
  private ParticleSystem explodedEffect;

  public ParticleSystem ExplodedEffect => explodedEffect;

}