using UnityEngine;

public class UISoundsProxy : MonoBehaviour
{
    public static void MouseClick()
    {
        UISounds.MouseClick();
    }

    public static void MouseHover()
    {
        UISounds.MouseHover();
    }

    public static void PressArcade()
    {
        UISounds.PressArcade();
    }

    public static void ButtonForwardTone()
    {
        UISounds.ButtonForwardTone();
    }

    public static void ButtonBackwardsTone()
    {
        UISounds.ButtonBackwardsTone();
    }

}