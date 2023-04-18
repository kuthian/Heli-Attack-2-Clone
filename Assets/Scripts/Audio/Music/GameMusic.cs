using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
  [SerializeField] private AK.Wwise.Event _wwGameMusic;

  [SerializeField] private AK.Wwise.State _wwGameplay;
  [SerializeField] private AK.Wwise.State _wwMainMenu;
  [SerializeField] private AK.Wwise.State _wwPaused;

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

  static public void Gameplay()
  {
    i._wwGameplay.SetValue();
  }

  static public void MainMenu()
  {
    i._wwMainMenu.SetValue();
  }

  static public void Paused()
  {
    i._wwPaused.SetValue();
  }

  public void Init()
  {
  }

  public void Init( AK.Wwise.State state )
  {
    state.SetValue();
  }

}
