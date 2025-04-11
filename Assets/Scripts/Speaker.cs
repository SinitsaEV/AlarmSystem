using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayAlarm()
    {
        _audioSource.Play();
    }

    public void StopAlarm()
    {
        _audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
