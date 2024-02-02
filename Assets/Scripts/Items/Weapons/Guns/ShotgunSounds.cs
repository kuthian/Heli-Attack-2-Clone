using UnityEngine;

public class ShotgunSounds : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event wwShotgunLoad;

    [SerializeField]
    private AK.Wwise.Event wwShotgunCock;

    public void ShotgunLoad()
    {
        wwShotgunLoad.Post(gameObject);
    }

    public void ShotgunCock()
    {
        wwShotgunCock.Post(gameObject);
    }

}