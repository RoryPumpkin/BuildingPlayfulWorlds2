using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerTopDown : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public GameObject wall;
    public GameObject enemy;

    private Vector3 spawnPos;
    private Rigidbody rb;
    private Vector3 inputVector;
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPos = transform.position;
    }

    
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        

        Vector3 accel = inputVector.normalized * moveSpeed;

        if (canJump && Input.GetKey("space"))
        {
            Debug.Log("you jumped");
            rb.AddForce(new Vector3(0,jumpForce,0));
            canJump = false;
        }

        rb.AddForce(accel * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            canJump = true;
        }

        if(collision.gameObject.layer == 10)
        {
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = spawnPos;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
