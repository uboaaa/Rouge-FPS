using UnityEngine;

public class FollowUI : MonoBehaviour
{
    public GameObject UI;

    // Update is called once per frame
    void Update()
    {
        transform.position = UI.transform.position;
    }
}
