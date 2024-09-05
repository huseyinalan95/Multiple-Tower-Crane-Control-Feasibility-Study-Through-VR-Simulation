using UnityEngine;

public class Hook : MonoBehaviour
{
    private GameObject currentCargo; // To keep track of the current cargo attached to the hook

    private void OnTriggerEnter(Collider col)
    {
        CargoContact cargoContact = col.GetComponent<CargoContact>();
        if (cargoContact != null && currentCargo == null)
        {
            currentCargo = col.gameObject;
            cargoContact.SetClampsVisibility(true); // Enable clamps when cargo is hooked
        }

        // Additional check to prevent recording drop-off when hook is empty
        TargetCargo targetCargo = col.GetComponent<TargetCargo>();
        if (targetCargo != null && currentCargo != null)
        {
            cargoContact = currentCargo.GetComponent<CargoContact>();
            if (cargoContact != null)
            {
                cargoContact.ReleaseCargo();
            }
            currentCargo = null; // Clear the current cargo as it's been dropped off
        }
    }
}
