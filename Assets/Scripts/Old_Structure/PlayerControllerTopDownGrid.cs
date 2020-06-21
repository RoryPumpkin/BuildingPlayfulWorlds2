using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerTopDownGrid : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4;
    [SerializeField]
    float gravity = 9.81f;

    List<Vector3> positions = new List<Vector3>();
    bool moving;

    void Start()
    {
        //spawnPos = targetPosition = startPosition = transform.position;
        positions.Add(transform.position);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveToDirection(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveToDirection(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveToDirection(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveToDirection(Vector3.right);
        }

        if (positions.Count > 2)
        {
            moving = true;
        }

        if (!moving) { return; }

        if (Vector3.Distance(positions[0], transform.position) > 1)
        {
            transform.position = positions[1];

            positions.Remove(positions[0]);

            if (positions.Count == 1)
            {
                moving = false;
            }
            else
            {

            }
        }

        if (positions.Count > 1)
        {
            transform.position += (positions[1] - positions[0]) * moveSpeed * Time.deltaTime;
        }

        /*
        //AXIS VERSION -------- So you can move by holding
        if(Input.GetAxisRaw("Vertical") == 1f)
        {
            targetPosition = transform.position + Vector3.forward;
            startPosition = transform.position;
            moving = true;
        }
        else if(Input.GetAxisRaw("Vertical") == -1f)
        {
            targetPosition = transform.position + Vector3.back;
            startPosition = transform.position;
            moving = true;
        }
        */

        //CheckDown();
    }

    void MoveToDirection(Vector3 dir)
    {
        positions.Add(positions[positions.Count - 1] + dir);
        doRaycast(dir);
        moving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void doRaycast(Vector3 dir)
    {
        Vector3 rayStart = transform.position + (dir * 0.5f);

        RaycastHit hit;

        if (Physics.Raycast(rayStart, dir, out hit, 1))
        {
            hit.collider.gameObject.GetComponent<OrbController>().Move(dir);
        }
    }

    void CheckDown()
    {
        Vector3 rayStart = transform.position + (Vector3.down*0.5f);

        RaycastHit hit;

        if (!Physics.Raycast(rayStart, Vector3.down, out hit, 0.5f))
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }
    }
}