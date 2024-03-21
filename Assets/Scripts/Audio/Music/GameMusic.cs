using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : PrefabSingleton<GameMusic>
{
    [Header("WWise Event")]
    [SerializeField] private AK.Wwise.Event wwGameMusic;

    [Header("WWise States")]
    [SerializeField] private AK.Wwise.State wwGameplay;
    [SerializeField] private AK.Wwise.State wwMainMenu;
    [SerializeField] private AK.Wwise.State wwPaused;

    private AK.Wwise.State initState;

    public static void Init(AK.Wwise.State state)
    {
        Instance.initState = state;
        Instance.initState.SetValue();
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        wwGameMusic.Post(gameObject);
        if (initState.IsValid())
        {
            initState.SetValue();
        }
    }

    static public void Gameplay()
    {
        Instance.wwGameplay.SetValue();
    }

    static public void MainMenu()
    {
        Instance.wwMainMenu.SetValue();
    }

    static public void Paused()
    {
        Instance.wwPaused.SetValue();
    }

}
