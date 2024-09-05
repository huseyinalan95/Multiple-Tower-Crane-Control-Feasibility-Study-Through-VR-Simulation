using UnityEngine;

public class Cargo : MonoBehaviour
{
    public GameObject clamps;
    public GameObject target;
    public CraneDataRecorder dataRecorder;
    public string cargoName; // Assign a unique name for each cargo in the Inspector
    private bool hasDroppedOff = false; // Flag to ensure drop-off is recorded only once

    private void Start()
    {
        // Find the data recorder if not assigned
        if (dataRecorder == null)
            dataRecorder = FindObjectOfType<CraneDataRecorder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetDropOff") && !hasDroppedOff)
        {
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;

            clamps.GetComponent<SkinnedMeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

            // Record drop-off time
            if (dataRecorder != null)
            {
                dataRecorder.RecordDropOff(cargoName, Time.time);
                hasDroppedOff = true; // Set flag to prevent multiple recordings
            }

            // Make trigger_target invisible
            other.gameObject.SetActive(false);
        }
    }
}
