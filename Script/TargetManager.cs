using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targets; // Assign in Inspector

    private int currentTargetIndex = 0;

    void Start()
    {
        // Initialize by activating only the first target and deactivating the rest
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].SetActive(i == 0);
        }
    }

    public void ActivateNextTarget()
    {
        if (currentTargetIndex < targets.Length)
        {
            targets[currentTargetIndex].SetActive(false);
            currentTargetIndex++;
            if (currentTargetIndex < targets.Length)
            {
                targets[currentTargetIndex].SetActive(true);
            }
        }
    }

}