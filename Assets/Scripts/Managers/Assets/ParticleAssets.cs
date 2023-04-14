using UnityEngine;

public class ParticleAssets : MonoBehaviour {
  
  private static ParticleAssets _i;

  public static ParticleAssets i {
    get {
      if (_i == null) {
        _i = (ParticleAssets) FindObjectOfType(typeof(ParticleAssets));
        if (_i == null) {
          _i = Instantiate(Resources.Load<ParticleAssets>("ParticleAssets"));
        }
      } 
      return _i;
    }
  }

  [SerializeField]
  private ParticleSystem _explodedEffect;

  public ParticleSystem ExplodedEffect => _explodedEffect;

  [SerializeField]
  private ParticleSystem _damagedPlayerEffect;

  public ParticleSystem DamagedPlayerEffect => _damagedPlayerEffect;

  [SerializeField]
  private ParticleSystem _explodedHeliEffect;

  public ParticleSystem ExplodedHeliEffect => _explodedHeliEffect;
 
}