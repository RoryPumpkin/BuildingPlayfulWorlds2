using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Particles : Unit
{
    public int particleType;

    public UnityEvent OnBecameTile;
    public UnityEvent EvolveEvent;
    public bool becomeATile;
    private Tile[] neighbours;
    private Particles fuseParticle;

    private void Awake()
    {
        fuseParticle = null;
    }

    public void Reset()
    {
        
    }

    protected override IEnumerator MoveToPosition(Vector3Int targetPosition, System.Action onDone = null)
    {
        yield return base.MoveToPosition(targetPosition, onDone);
    }

    public void Attack()
    {

    }

    public void BecomeTile()
    {
        OnBecameTile?.Invoke();
    }

    public void SetInActive()
    {
        gameObject.SetActive(false);
    }

    public override void MoveInDirection(Vector3Int direction)
    {
        Level grid = GameController.Instance.Level;
        Tile tile = grid.GetTileForPosition(transform.position + direction - Vector3Int.up);
        if (tile != null)
        {
            Debug.Log("move onto tile");
            if (tile.IsFree())
            {
                if (CurrentTile != null)
                {
                    CurrentTile.ReleaseUnit();
                }
                tile.AssignUnit(this);
                CurrentTile = tile;

                neighbours = base.CurrentTile.GetNeighbours();

                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (neighbours[i].Unit == null) { continue; }
                    if (neighbours[i].Unit.type == Type.Player) { continue; }

                    if (neighbours[i].Unit.type == Unit.Type.Particle)
                    {
                        if (neighbours[i].Unit.GetComponent<Particles>().particleType == particleType)
                        {
                            fuseParticle = neighbours[i].Unit.GetComponent<Particles>();
                        }
                    }
                }

                if (fuseParticle != null)
                {
                    StartCoroutine(MoveToPosition(tile.Position, EvolveParticle));
                }
                else
                {
                    StartCoroutine(MoveToPosition(tile.Position));
                }
            }

            
        }
        else
        {
            if (CurrentTile != null)
            {
                CurrentTile.ReleaseUnit();
            }
            //LookAtPosition(CurrentTile.Position - direction);
            StartCoroutine(MoveToPosition(CurrentTile.Position + direction, BecomeTile));
            CurrentTile = null;
        }
    }

    public void EvolveParticle()
    {
        EvolveEvent?.Invoke();
        //in the event we want to spawn a particleType+1
        //then we want to remove this particle and the one it fused with
        gameObject.SetActive(false);
        fuseParticle.gameObject.SetActive(false);
        fuseParticle = null;
    }
}
