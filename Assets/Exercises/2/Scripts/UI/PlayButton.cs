using UnityEngine;


public class PlayButton : MonoBehaviour
{
    public void OnClickPlay()
    {
        //DO Play
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}