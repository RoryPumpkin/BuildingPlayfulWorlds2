using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent PlayerWin;

    private GameObject player;
    private bool won;
    
    void Start()
    {
        player = GameController.Instance.Player.gameObject;
    }

    void Update()
    {
        if (Mathf.Approximately(Vector3.Distance(player.transform.position, transform.position), 1f) && !won)
        {
            PlayerWin?.Invoke();
            won = true;
        }
    }

    public void Victory()
    {
        GameController.Instance.WinGame();
    }
}
