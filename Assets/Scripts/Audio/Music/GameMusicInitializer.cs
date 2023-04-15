using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicInitializer : MonoBehaviour
{
  [SerializeField] private AK.Wwise.State _state;

  void Start()
  {
    if (_state.IsValid())
    {
      GameMusic.i.Init(_state);
    }
    else 
    {
      GameMusic.i.Init();
    }
  }
}
