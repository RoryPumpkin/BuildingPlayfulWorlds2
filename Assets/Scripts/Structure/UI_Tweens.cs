using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Tweens : MonoBehaviour
{
    public enum State { nothing, retry, full}
    public State currentState = State.retry;

    public Vector3 pos;

    void Awake()
    {
        pos = gameObject.transform.position;
    }

    
    void Update()
    {
        
    }

    public void MoveToPos(GameObject obj, Vector3 pos, float time)
    {
        LeanTween.move(obj, pos, time);
    }

}
