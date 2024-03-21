using UnityEngine;

public class UISounds : PrefabSingleton<UISounds>
{
    [SerializeField]
    private AK.Wwise.Event wwMouseClick;

    [SerializeField]
    private AK.Wwise.Event wwMouseHover;

    [SerializeField]
    private AK.Wwise.Event wwPressArcade;

    [SerializeField]
    private AK.Wwise.Event wwButtonForwardTone;

    [SerializeField]
    private AK.Wwise.Event wwButtonBackwardsTone;

    [SerializeField]
    private AK.Wwise.Event wwLeaderboardArrows;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MouseClick()
    {
        wwMouseClick.Post(gameObject);
    }

    public void MouseHover()
    {
        wwMouseHover.Post(gameObject);
    }

    public void PressArcade()
    {
        wwPressArcade.Post(gameObject);
    }

    public void ButtonForwardTone()
    {
        wwButtonForwardTone.Post(gameObject);
    }

    public void ButtonBackwardsTone()
    {
        wwButtonBackwardsTone.Post(gameObject);
    }

    public void LeaderboardArrows()
    {
        wwLeaderboardArrows.Post(gameObject);
    }
}