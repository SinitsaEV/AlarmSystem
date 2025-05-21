using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Action AlarmTriggered;
    public Action OnZoneDeactivated;

    private void OnTriggerEnter(Collider other)
    {
        AlarmTriggered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnZoneDeactivated?.Invoke();
    }
}
