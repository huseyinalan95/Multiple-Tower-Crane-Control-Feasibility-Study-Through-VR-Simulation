using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CraneDataRecorder : MonoBehaviour
{
    private class CraneDataEntry
    {
        public string CraneName;
        public string CargoName;
        public float PickUpTime;
        public float DropOffTime;

        public float TotalTime => DropOffTime - PickUpTime;

        public CraneDataEntry(string craneName, string cargoName, float pickUpTime)
        {
            CraneName = craneName;
            CargoName = cargoName;
            PickUpTime = pickUpTime;
        }

        public void SetDropOffTime(float dropOffTime)
        {
            DropOffTime = dropOffTime;
        }
    }

    private Dictionary<string, CraneDataEntry> dataEntries = new Dictionary<string, CraneDataEntry>();
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "CraneData.csv");
    }

    public void RecordPickUp(string craneName, string cargoName, float pickUpTime) // Updated method
    {
        if (!dataEntries.ContainsKey(cargoName))
        {
            dataEntries.Add(cargoName, new CraneDataEntry(craneName, cargoName, pickUpTime));
        }
        else
        {
            // Update existing entry with new pick up time
            dataEntries[cargoName].PickUpTime = pickUpTime;
            dataEntries[cargoName].CraneName = craneName; // Update crane name if different
        }
        Debug.Log("Pick-up recorded for cargo: " + cargoName);
    }

    public void RecordDropOff(string cargoName, float dropOffTime)
    {
        if (dataEntries.TryGetValue(cargoName, out var entry))
        {
            entry.SetDropOffTime(dropOffTime);
            Debug.Log("Drop-off recorded for cargo: " + cargoName);
        }
        else
        {
            Debug.LogWarning("Drop-off record failed for cargo: " + cargoName);
        }
    }

    private void OnApplicationQuit()
    {
        ExportDataToCSV();
    }

    private void ExportDataToCSV()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Crane Name,Cargo Name,Pick Up Time,Drop Off Time,Total Time");

        foreach (var entry in dataEntries.Values)
        {
            sb.AppendLine($"{entry.CraneName},{entry.CargoName},{entry.PickUpTime},{entry.DropOffTime},{entry.TotalTime}");
        }

        File.WriteAllText(filePath, sb.ToString());
        Debug.Log("Data exported to: " + filePath);
    }
}
