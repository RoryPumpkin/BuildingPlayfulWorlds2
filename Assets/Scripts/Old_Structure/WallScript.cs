using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody rb;
    private Vector3 move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        move.x = moveSpeed;
        rb.velocity = move;
    }
}
