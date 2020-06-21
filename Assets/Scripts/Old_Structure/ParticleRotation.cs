using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float minRotationSpeed;

    float x;

    void Start()
    {
        
    }

    void Update()
    {
        rotationSpeed = Mathf.SmoothStep(rotationSpeed, minRotationSpeed, Time.deltaTime * 1f);
        x += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,x,0);
    }
}
