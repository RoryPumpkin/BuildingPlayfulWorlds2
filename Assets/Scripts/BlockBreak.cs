using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer mr;
    private MeshRenderer parentMr;
    private CrackChilds crackParent;
    private bool moving;

    public float shrink = 0.99f;

    void Start()
    {
        crackParent = transform.parent.GetComponent<CrackChilds>();
        rb = GetComponent<Rigidbody>();
        //mr = GetComponent<MeshRenderer>();
        //parentMr = transform.parent.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(moving && transform.localScale.x > 0.01f)
        {
            shrink = Mathf.SmoothStep(shrink, 0.7f, Time.deltaTime * 1f);
            transform.localScale *= shrink;
        }
        else if(transform.localScale.x <= 0.01f)
        {
            crackParent.childs.Remove(this.gameObject);
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = false;
        moving = true;
    }

    public void UpdateMaterial()
    {
        Debug.Log("change mat");
        mr.material = parentMr.material;
    }
}
