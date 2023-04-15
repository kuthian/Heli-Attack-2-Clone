using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
  [SerializeField] private AK.Wwise.Event _wwGameMusic;

  private static GameMusic _i;

  public static GameMusic i {
    get {
      _i = (GameMusic) FindObjectOfType(typeof(GameMusic));
      if (_i == null) {
        _i = Instantiate(Resources.Load<GameMusic>("GameMusic"));
      }
      return _i;
    }
  }

  void Start()
  {
    name = "GameMusic";
    _wwGameMusic.Post(gameObject);
    DontDestroyOnLoad(gameObject);
  }

  public void Init()
  {
  }

}
