using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float rotationAmount = 5f; // �h��̊p�x
    public float speed = 5f; // �h��̑���
    private float _randomOffset;

    void Start()
    {
        _randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float angle = rotationAmount * Mathf.Sin(Time.time * speed + _randomOffset);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}