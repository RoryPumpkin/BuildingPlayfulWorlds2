using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (FindObjectOfType<UI_Tweens>())
        {
            UI_Tweens won = FindObjectOfType<UI_Tweens>();
            Debug.Log("Found UI Tween object in playerTurn: " + won.name);
            won.currentState = UI_Tweens.State.nothing;
            won.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
