using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : Unit
{
    public UnityEvent OnPlayerDiedEvent;

    private void Awake()
    {
    }

    protected override IEnumerator MoveToPosition(Vector3Int targetPosition, System.Action onDone = null)
    {
        yield return base.MoveToPosition(targetPosition, onDone);
    }

}
