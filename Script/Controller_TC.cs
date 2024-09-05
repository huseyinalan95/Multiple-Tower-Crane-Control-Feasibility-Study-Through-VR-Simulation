using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure to include the OVR namespace if using Oculus Platform
using Oculus.Platform;

// Crane control script
public class Controller_TC : MonoBehaviour
{
    // Public fields to be assigned in the Inspector
    public bool isEnabled = false;

    // Rigidbody components for different parts of the crane
    public Rigidbody boom_point_Rotation;
    public Rigidbody truck;
    public Rigidbody hook;
    public Rigidbody hook_point_Rotation;
    public ConfigurableJoint joint;

    // Speed settings for different crane movements
    public float speed_General = 0.01f;
    public float speed_TC_Rails = 4f;
    public float speed_Boom_Rotation = 0.25f;
    public float speed_Truck = 5f;
    public float speed_Hook = 5f;
    public float speed_Hook_Rotation = 0.25f;

    // AudioSources for different crane sounds
    public AudioSource boomRotationSound;  // Assign in Inspector
    public AudioSource trackMovementSound; // Assign in Inspector
    public AudioSource hookMovementSound;  // Assign in Inspector

    private float distance = 0f; // Initial hook position

    void FixedUpdate()
    {
        if (!isEnabled) return;

        // Crane boom rotation
        bool isBoomRotating = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
                              OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch) ||
                              OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch);
        HandleCraneSound(boomRotationSound, isBoomRotating);

        // Movement of the track along the boom
        bool isTrackMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                             OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) ||
                             OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        HandleCraneSound(trackMovementSound, isTrackMoving);

        // Hook height control
        bool isHookMoving = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) ||
                            OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch) ||
                            OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch);
        HandleCraneSound(hookMovementSound, isHookMoving);

        // Movement by pressing keys

        // Crane boom rotation
        if (Input.GetKey(KeyCode.RightArrow) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch))
        {
            boom_point_Rotation.AddRelativeTorque(0f, speed_Boom_Rotation * speed_General, 0f, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch))
        {
            boom_point_Rotation.AddRelativeTorque(0f, -speed_Boom_Rotation * speed_General, 0f, ForceMode.VelocityChange);
        }

        // Movement of the track along the boom
        if (Input.GetKey(KeyCode.W) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            truck.AddRelativeForce(0f, 0f, speed_Truck * speed_General, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            truck.AddRelativeForce(0f, 0f, -speed_Truck * speed_General, ForceMode.VelocityChange);
        }

        // Hook rotation
        if (Input.GetKey(KeyCode.E) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            hook_point_Rotation.AddRelativeTorque(0f, speed_Hook_Rotation * speed_General, 0f, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.Q) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            hook_point_Rotation.AddRelativeTorque(0f, -speed_Hook_Rotation * speed_General, 0f, ForceMode.VelocityChange);
        }

        // Hook height control
        if (Input.GetKey(KeyCode.DownArrow) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            if (hook.transform.position.y >= 0.3)
            {
                distance += speed_Hook * speed_General;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch))
        {
            if (distance >= 0)
            {
                distance -= speed_Hook * speed_General;
            }
        }
        SoftJointLimit limit = joint.linearLimit;
        limit.limit = distance;
        joint.linearLimit = limit;

        // To work correctly, you need to wake up Rigidbody
        truck.GetComponent<Rigidbody>().WakeUp();
        hook.GetComponent<Rigidbody>().WakeUp();
    }

    void HandleCraneSound(AudioSource audioSource, bool shouldPlay)
    {
        if (shouldPlay)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}