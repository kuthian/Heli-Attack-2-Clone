using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event wwGameMusic;

    [SerializeField] private AK.Wwise.State wwGameplay;
    [SerializeField] private AK.Wwise.State wwMainMenu;
    [SerializeField] private AK.Wwise.State wwPaused;

    private AK.Wwise.State _initState;

    private static GameMusic _i;

    public static GameMusic i
    {
        get
        {
            _i = (GameMusic)FindObjectOfType(typeof(GameMusic));
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameMusic>("GameMusic"));
            }
            return _i;
        }
    }

    public void Init()
    {
    }

    public void Init(AK.Wwise.State state)
    {
        _initState = state;
        _initState.SetValue();
    }

    void Start()
    {
        name = "GameMusic";
        DontDestroyOnLoad(gameObject);
        wwGameMusic.Post(gameObject);
        if (_initState.IsValid())
        {
            _initState.SetValue();
        }
    }

    static public void Gameplay()
    {
        i.wwGameplay.SetValue();
    }

    static public void MainMenu()
    {
        i.wwMainMenu.SetValue();
    }

    static public void Paused()
    {
        i.wwPaused.SetValue();
    }

}
