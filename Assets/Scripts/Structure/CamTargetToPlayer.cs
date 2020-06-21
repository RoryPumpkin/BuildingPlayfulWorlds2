using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTargetToPlayer : MonoBehaviour
{
    public GameObject midObject;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(GameController.Instance.Player)
        {
            gameObject.transform.position = Vector3.Lerp(GameController.Instance.Player.transform.position, midObject.transform.position, 0.3f);
        }
    }
}
