using UnityEngine;

public class CargoContact : MonoBehaviour
{
    public GameObject Point_Rotation_Hook;
    public GameObject clamps;
    public GameObject trigger_ancoragePoint;
    public CraneDataRecorder dataRecorder;
    public string cargoName; // Assign a unique name for each cargo in the Inspector
    public string craneName; // Variable to set crane name in Inspector

    private bool contactHook = false;
    private float pickUpTime;

    private void Start()
    {
        // Find the data recorder if not assigned in the Inspector
        if (dataRecorder == null)
        {
            dataRecorder = FindObjectOfType<CraneDataRecorder>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Point_Rotation_Hook && !contactHook)
        {
            contactHook = true;
            pickUpTime = Time.time; // Record pick-up time
            trigger_ancoragePoint.GetComponent<MeshRenderer>().enabled = false;
            SetClampsVisibility(true); // Enable clamps when cargo is hooked

            // Record the pickup event
            if (dataRecorder != null)
            {
                dataRecorder.RecordPickUp(craneName, cargoName, pickUpTime); // Pass craneName here
                Debug.Log("Cargo picked up at: " + pickUpTime);
            }
        }
    }

    void FixedUpdate()
    {
        if (contactHook)
        {
            // Logic to follow the hook
            transform.position = Point_Rotation_Hook.transform.position;
            transform.rotation = Point_Rotation_Hook.transform.rotation;
        }
    }

    public void SetClampsVisibility(bool isVisible)
    {
        clamps.GetComponent<SkinnedMeshRenderer>().enabled = isVisible;
    }

    public void ReleaseCargo()
    {
        // Logic for releasing the cargo, if needed
        if (contactHook)
        {
            contactHook = false;
            SetClampsVisibility(false); // Disable clamps when cargo is released
            trigger_ancoragePoint.GetComponent<Rigidbody>().isKinematic = false;
            Point_Rotation_Hook.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
