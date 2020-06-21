using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    //should be called from a static script
    [SerializeField]
    float moveSpeed = 4;
    [SerializeField]
    LayerMask rayMask;
    [SerializeField]
    float gravity = 9.81f;

    List<Vector3> positions = new List<Vector3>();
    bool moving;

    void Start()
    {
        positions.Add(transform.position);
    }

    void Update()
    {
        if (!moving) { return; }

        if (Vector3.Distance(positions[0], transform.position) > 1)
        {
            transform.position = positions[1];

            positions.Remove(positions[0]);

            if (positions.Count == 1)
            {
                moving = false;
            }
        }

        if (positions.Count > 1)
        {
            transform.position += (positions[1] - positions[0]) * moveSpeed * Time.deltaTime;
        }

        //CheckDown();
    }

    public void Move(Vector3 direction)
    {
        positions.Add(positions[positions.Count - 1] + direction);
        doRaycast(direction);
        moving = true;
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
        Vector3 rayStart = transform.position + (Vector3.down * 0.5f);

        RaycastHit hit;

        if (!Physics.Raycast(rayStart, Vector3.down, out hit, 0.5f))
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }
    }
}
