using System;
using UnityEngine;
using UnityEngine.Events;

public class QTEMinigame : MonoBehaviour
{
    [SerializeField] private float _barDuration = 2.0f;
    [SerializeField] private float _errorBuffer = 0.25f;

    private float _markerTime;
    private float _incrementDirection = 1f;
    
    public UnityEvent onSuccess;
    public UnityEvent<float> onUpdateMarker;

    void Update()
    {
        _markerTime += _incrementDirection * Time.deltaTime;
        if (_markerTime >= _barDuration)
        {
            _incrementDirection = -1f;
        }
        else if (_markerTime <= 0f)
        {
            _incrementDirection = 1f;
        }
        onUpdateMarker.Invoke(Math.Clamp(_markerTime / _barDuration, 0f, 1f));
    }

    public void Press()
    {
        if (Math.Abs(_markerTime - (_barDuration / 2f)) < _errorBuffer)
        {
            onSuccess.Invoke();
        }
    }
}
