using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicInitializer : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event myEvent;

    private void Start()
    {
        myEvent.Post(gameObject);
    }

}
