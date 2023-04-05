using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInitializer : MonoBehaviour
{
  [SerializeField] private AK.Wwise.Event _wwGameStart;
  
  private void Start()
  {
    _wwGameStart.Post(gameObject);
  }

}