using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Tile CurrentTile { get; set; }
    public bool moving;
    public Type type;

    public enum Type { Player, Particle, Block };

    public void Init()
    {
        Level grid = GameController.Instance.Level;
        Tile tile = grid.GetTileForPosition(transform.position - Vector3.up);
        if(tile != null && tile.Unit == null)
        {
            //Debug.Log("Assigned Unit: " + name + " To tile: " + tile.name);
            tile.AssignUnit(this);
            CurrentTile = tile;
        }
    }

    public void isTileFree()
    {

    }

    public void MoveToTile(Tile tile)
    {
        if (tile.IsFree())
        {
            
        }

        if (CurrentTile != null)
        {
            CurrentTile.ReleaseUnit();
        }
        
        tile.AssignUnit(this);
 
        CurrentTile = tile;

        StartCoroutine(MoveToPosition(tile.Position));
    }

    public virtual void MoveInDirection(Vector3Int direction)
    {
        Level grid = GameController.Instance.Level;
        Tile tile = grid.GetTileForPosition(transform.position + direction - Vector3Int.up);
        if (tile != null)
        {
            if (tile.IsFree())
            {
                if (CurrentTile != null)
                {
                    CurrentTile.ReleaseUnit();
                }
                tile.AssignUnit(this);
                CurrentTile = tile;
                
                StartCoroutine(MoveToPosition(tile.Position));
            }
        }
        else
        {
            if (CurrentTile != null)
            {
                CurrentTile.ReleaseUnit();
            }
            StartCoroutine(MoveToPosition(CurrentTile.Position + direction));
            CurrentTile = null;
        }
    }

    protected virtual IEnumerator MoveToPosition(Vector3Int targetPosition, System.Action onDone = null)
    {
        moving = true;
        Vector3 targetPos = targetPosition + Vector3Int.up;
        Vector3 startPosition = transform.position;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPos, t);
            yield return null;
        }
        transform.position = targetPos;
        moving = false;
        onDone?.Invoke();
    }

    public void LookAtPosition(Vector3Int position)
    {
        transform.rotation = Quaternion.LookRotation((position - transform.position).normalized, Vector3.up);
    }
}
