using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private Speaker _speaker;
    [SerializeField] private Sensor _sensor;
    [SerializeField] private float _fadeSpeed = 0.1f;

    private float _targetVolume;
    private float _currentVolume;
    private float _maxVolume = 1;
    private float _minVolume = 0;

    private Coroutine _volumeChangerCoroutine;

    private void Awake()
    {
        if (_sensor != null)
        {
            _sensor.AlarmTriggered += OnAlarmTriggered;
            _sensor.OnZoneDeactivated += OnZoneDeactivated;
        }
    }

    private void OnDestroy()
    {
        if (_sensor != null)
        {
            _sensor.AlarmTriggered -= OnAlarmTriggered;
            _sensor.OnZoneDeactivated -= OnZoneDeactivated;
        }
    }

    private void OnAlarmTriggered()
    {
        _speaker.PlayAlarm();

        SetTargetVolume(_maxVolume);
    }

    private void OnZoneDeactivated()
    {
        SetTargetVolume(_minVolume);
    }

    private IEnumerator VolumeChanger()
    {
        while (_currentVolume != _targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _targetVolume, _fadeSpeed * Time.deltaTime);

            _speaker.SetVolume(_currentVolume);

            yield return null;
        }

        if (Mathf.Approximately(_currentVolume, _minVolume))
        {
            _speaker.StopAlarm();
        }

        _volumeChangerCoroutine = null;
    }

    private void SetTargetVolume(float targetVolume)
    {
        if(_targetVolume == targetVolume)
            return;

        _targetVolume = targetVolume;

        if (_volumeChangerCoroutine == null)
            _volumeChangerCoroutine = StartCoroutine(VolumeChanger());        
    }    
}
