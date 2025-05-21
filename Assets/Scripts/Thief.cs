using System.Collections;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _reachedDistance = 0.1f;
    [SerializeField] private float _delayBeforeTurn = 2f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private WaitForSecondsRealtime _waitForTime;
    private bool _isWaiting;


    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _direction = -_transform.forward;
        _startPosition = _transform.position;
        _endPosition = _transform.position + _direction * _distance;
        _waitForTime = new WaitForSecondsRealtime(_delayBeforeTurn);
    }

    private void Update()
    {
        if (_isWaiting)
            return;

        _transform.position = Vector3.MoveTowards(_transform.position, _endPosition, Time.deltaTime * _speed);

        if ((_transform.position - _endPosition).sqrMagnitude < _reachedDistance * _reachedDistance)
        {
            StartCoroutine(DelayDirectionChange());
        }
    }

    private IEnumerator DelayDirectionChange()
    {
        _isWaiting = true;

        (_endPosition, _startPosition) = (_startPosition, _endPosition);

        yield return _waitForTime;

        _isWaiting = false;
    }
}
