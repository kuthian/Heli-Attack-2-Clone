using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event _wwMouseClick;

    [SerializeField]
    private AK.Wwise.Event _wwMouseHover;

    [SerializeField]
    private AK.Wwise.Event _wwPressArcade;

    [SerializeField]
    private AK.Wwise.Event _wwButtonForwardTone;

    [SerializeField]
    private AK.Wwise.Event _wwButtonBackwardsTone;

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
        i._wwMouseClick.Post(i.gameObject);
    }

    public static void MouseHover()
    {
        i._wwMouseHover.Post(i.gameObject);
    }

    public static void PressArcade()
    {
        i._wwPressArcade.Post(i.gameObject);
    }

    public static void ButtonForwardTone()
    {
        i._wwButtonForwardTone.Post(i.gameObject);
    }

    public static void ButtonBackwardsTone()
    {
        i._wwButtonBackwardsTone.Post(i.gameObject);
    }
}