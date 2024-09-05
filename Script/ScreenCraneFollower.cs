using UnityEngine;

public class ScreenCraneFollower : MonoBehaviour
{
    public Transform target; // Assign the target object in the Inspector

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}