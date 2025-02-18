using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMover : MonoBehaviour
{
    public float minValue;
    public float maxValue;
    public float speed = 1.0f;

    private float _startValue;
    private float _endValue;
    private float _t;

    private float _value;
    private Vector3 _waterPosition;
    
    private void Start()
    {
        _startValue = minValue;
        _endValue = maxValue;
        _waterPosition = transform.position;
    }

    private void Update()
    {
        _t += Time.deltaTime * speed;
        _value = Mathf.Lerp(_startValue, _endValue, Mathf.PingPong(_t, 1));

        transform.localPosition = new Vector3(_waterPosition.x, _waterPosition.y + _value, _waterPosition.z);
    }
}
