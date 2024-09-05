using UnityEngine;

public class AncoragePointManager : MonoBehaviour
{
    public GameObject[] ancoragePoints; // Assign in the Inspector
    private int currentAncoragePointIndex = 0;

    void Start()
    {
        InitializeAncoragePoints();
    }

    public void ActivateNextAncoragePoint()
    {
        if (currentAncoragePointIndex < ancoragePoints.Length)
        {
            SetAncoragePointVisibility(ancoragePoints[currentAncoragePointIndex], false);
            currentAncoragePointIndex++;
            if (currentAncoragePointIndex < ancoragePoints.Length)
            {
                SetAncoragePointVisibility(ancoragePoints[currentAncoragePointIndex], true);
            }
        }
    }

    private void InitializeAncoragePoints()
    {
        for (int i = 0; i < ancoragePoints.Length; i++)
        {
            SetAncoragePointVisibility(ancoragePoints[i], i == 0);
        }
    }

    private void SetAncoragePointVisibility(GameObject ancoragePoint, bool isVisible)
    {
        if (ancoragePoint != null)
        {
            ancoragePoint.SetActive(isVisible);
        }
    }
}
