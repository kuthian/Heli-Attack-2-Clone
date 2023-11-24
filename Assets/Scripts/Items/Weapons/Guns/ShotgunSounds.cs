using UnityEngine;

public class ShotgunSounds : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event _wwShotgunLoad;

    [SerializeField]
    private AK.Wwise.Event _wwShotgunCock;

    public void ShotgunLoad()
    {
        _wwShotgunLoad.Post(gameObject);
    }

    public void ShotgunCock()
    {
        _wwShotgunCock.Post(gameObject);
    }

}