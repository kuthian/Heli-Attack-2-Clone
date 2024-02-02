using UnityEngine;

public class UISounds : MonoBehaviour
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

    private static UISounds _i;



    public static UISounds i
    {
        get
        {
            _i = (UISounds)FindObjectOfType(typeof(UISounds));
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<UISounds>("UISounds"));
            }
            return _i;
        }
    }

    void Start()
    {
        name = "UISounds";
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
    }

    public static void MouseClick()
    {
        i.wwMouseClick.Post(i.gameObject);
    }

    public static void MouseHover()
    {
        i.wwMouseHover.Post(i.gameObject);
    }

    public static void PressArcade()
    {
        i.wwPressArcade.Post(i.gameObject);
    }

    public static void ButtonForwardTone()
    {
        i.wwButtonForwardTone.Post(i.gameObject);
    }

    public static void ButtonBackwardsTone()
    {
        i.wwButtonBackwardsTone.Post(i.gameObject);
    }
}