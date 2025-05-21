using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Action AlarmTriggered;
    public Action ZoneDeactivated;

    private void OnTriggerEnter(Collider other)
    {
        AlarmTriggered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        ZoneDeactivated?.Invoke();
    }
}
