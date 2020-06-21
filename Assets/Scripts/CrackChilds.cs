using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackChilds : MonoBehaviour
{
    public bool crack = true;
    public List<GameObject> childs;
    void Start()
    {
        GetChildren();
    }

    void Update()
    {
        
    }

    public void ChildCrack()
    {
        if(childs == null) { return; }
        for (int i = 0; i < childs.Count-1; i++)
        {
            childs[i].transform.localScale = Vector3.Lerp(childs[i].transform.localScale, new Vector3(80, 80, 80), Time.deltaTime * 2f);
            if(childs[i].transform.localScale == new Vector3(80, 80, 80))
            {
                crack = false;
                return;
            }
        }
    }

    public void UnCrack()
    {
        if (childs == null) { return; }
        for (int i = 0; i < childs.Count - 1; i++)
        {
            childs[i].transform.localScale = new Vector3(95f, 95f, 95f);
            crack = false;
        }
    }

    public void GetChildren()
    {
        //childs.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<BlockBreak>())
            {
                GameObject c = gameObject.transform.GetChild(i).gameObject;
                childs.Add(c);
            }
        }
    }
}
