using UnityEngine;

public class TargetCargo : MonoBehaviour
{
    public CraneDataRecorder dataRecorder;

    private void Start()
    {
        // Find the data recorder if not assigned
        if (dataRecorder == null)
            dataRecorder = FindObjectOfType<CraneDataRecorder>();
    }

    private void OnTriggerEnter(Collider col)
    {
        CargoContact cargoContact = col.GetComponent<CargoContact>();
        if (cargoContact != null)
        {
            // Record drop-off time
            if (dataRecorder != null)
                dataRecorder.RecordDropOff(cargoContact.cargoName, Time.time);
        }
    }
}
