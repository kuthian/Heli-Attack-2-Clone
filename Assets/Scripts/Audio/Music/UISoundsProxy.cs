using UnityEngine;

public class UISoundsProxy : MonoBehaviour
{
    public static void MouseClick()
    {
        UISounds.Instance.MouseClick();
    }

    public static void MouseHover()
    {
        UISounds.Instance.MouseHover();
    }

    public static void PressArcade()
    {
        UISounds.Instance.PressArcade();
    }

    public static void ButtonForwardTone()
    {
        UISounds.Instance.ButtonForwardTone();
    }

    public static void ButtonBackwardsTone()
    {
        UISounds.Instance.ButtonBackwardsTone();
    }

    public static void LeaderboardArrows()
    {
        UISounds.Instance.LeaderboardArrows();
    }

}