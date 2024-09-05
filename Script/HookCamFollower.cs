using UnityEngine;

public class HookCamFollower : MonoBehaviour
{
    public Transform target; // Assign the target object in the Inspector

    void Update()
    {
        if (target != null)
        {
            // Update the position to follow the target
            transform.position = target.position;

            // Maintain the camera's predefined rotation
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}