using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _up = 1;

    private void Update()
    {
        Vector3 _position = new Vector3(_target.transform.position.x, _target.transform.position.y + +_up, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, _position , Time.deltaTime * _speed);
    }
}
