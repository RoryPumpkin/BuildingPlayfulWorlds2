using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpSlow : MonoBehaviour
{
    public float speed;
    public float increase;

    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.y < 2)
        {
            speed *= increase;
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == 13)
        {
            gameObject.SetActive(false);
        }
    }
}
