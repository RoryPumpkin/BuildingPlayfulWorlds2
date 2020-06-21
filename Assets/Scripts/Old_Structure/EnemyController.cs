using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Idle, Chasing, Firing
    }
    private State state;

    public float moveSpeed;
    public float fireDist;
    public Color idleColor;

    private Material m;
    private Color activeColor;
    private Rigidbody rb;
    private Vector2 rot;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = State.Idle;
        m = GetComponent<MeshRenderer>().material;
        activeColor = m.color;
        m.color = idleColor;
    }

    
    void Update()
    {
        if (player != null && (player.transform.position - transform.position).magnitude < fireDist && state == State.Chasing)
        {
            Vector3 vec_to_player = (player.transform.position - transform.position).normalized;
            m.color = activeColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && player == null)
        {
            player = other.gameObject;
        }

        if (other.gameObject == player)
        {
            state = State.Chasing;
            rb.velocity = (other.gameObject.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(player == null && other.gameObject.layer == 11)
        {
            player = other.gameObject;
        }

        if(other.gameObject == player)
        {
            state = State.Chasing;
            rb.velocity = (other.gameObject.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
    }
}
