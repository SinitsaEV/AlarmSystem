using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private List<Speaker> _speakers;
    [SerializeField] private List<Sensor> _sensors;

    private float _targetVolume;
    private float _currentVolume;
    private float _maxVolume = 1;
    private float _minVolume = 0;
    private float _speed = 0.1f;

    private void Awake()
    {
        foreach (Sensor sensor in _sensors)
        {
            sensor.AlarmTriggered += OnAlarmTriggered;
            sensor.OnZoneDeactivated += OnZoneDeactivated;
        }
    }

    private void Update()
    {
        
    }

    private void OnAlarmTriggered()
    {
        foreach (Speaker speaker in _speakers)
            speaker.PlayAlarm();       
        
        _targetVolume = _maxVolume;

        StartCoroutine(VolumeChanger());
    }

    private IEnumerator VolumeChanger()
    {
        while (_currentVolume != _targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _targetVolume, _speed * Time.deltaTime);

            foreach (Speaker speaker in _speakers)
                speaker.SetVolume(_currentVolume);

            yield return null;
        }
    }

    private void OnZoneDeactivated()
    {
        _targetVolume = _minVolume;
        StartCoroutine(VolumeChanger());
    }
}
