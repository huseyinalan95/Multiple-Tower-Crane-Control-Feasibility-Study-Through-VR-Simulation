using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CraneSwitcher : MonoBehaviour
{
    public GameObject OVRCameraRig; // Assign this in the inspector
    public GameObject[] Cranes;     // Assign the cranes in the inspector
    public Vector3[] CranePositions; // World positions for each crane
    public Vector3[] CraneRotations; // World rotations for each crane (in Euler angles)

    private int currentCraneIndex = 0;
    private InputDevice leftHandController;

    void Start()
    {
        var leftHandedControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandedControllers);
        if (leftHandedControllers.Count > 0)
        {
            leftHandController = leftHandedControllers[0];
        }
    }

    void Update()
    {
        // Switch to the next crane
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.N) || WasButtonPressed(OVRInput.Button.One))
        {
            SwitchCrane(true);
        }

        // Switch to the previous crane
        if (Input.GetKeyDown(KeyCode.B) || WasButtonPressed(OVRInput.Button.Two))
        {
            SwitchCrane(false);
        }
    }

    bool WasButtonPressed(OVRInput.Button button)
    {
        return OVRInput.GetDown(button, OVRInput.Controller.LTouch);
    }

    void SwitchCrane(bool forward)
    {
        if (forward)
        {
            currentCraneIndex = (currentCraneIndex + 1) % Cranes.Length;
        }
        else
        {
            currentCraneIndex = (currentCraneIndex - 1 + Cranes.Length) % Cranes.Length;
        }

        // Disable the control script on all cranes
        foreach (var crane in Cranes)
        {
            crane.GetComponent<Controller_TC>().isEnabled = false;
        }

        // Enable the control script on the current crane
        Cranes[currentCraneIndex].GetComponent<Controller_TC>().isEnabled = true;

        // Detach the camera from its current parent
        OVRCameraRig.transform.SetParent(null);

        // Set the camera's world position and rotation to the new crane's cabin position
        OVRCameraRig.transform.position = CranePositions[currentCraneIndex];
        OVRCameraRig.transform.eulerAngles = CraneRotations[currentCraneIndex];

        // Reattach the camera to the new crane
        OVRCameraRig.transform.SetParent(Cranes[currentCraneIndex].transform, false);
    }
}
