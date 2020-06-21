using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTurnState : State
{
    public LayerMask tileLayer;

    private List<Particles> particles = new List<Particles>();
    private List<Unit> move = new List<Unit>();
    private Vector3Int direction;
    private bool thereIsANextTile;

    public override void InitState(FSM fsm)
    {
        if (FindObjectOfType<UI_Tweens>())
        {
            UI_Tweens won = FindObjectOfType<UI_Tweens>();
            won.currentState = UI_Tweens.State.retry;
            won.MoveToPos(won.gameObject, won.pos, 0.3f);
        }

        base.InitState(fsm);
        particles.Clear();
        particles = FindObjectsOfType<Particles>().ToList();
    }

    public override void OnEnter()
    {
        //Debug.Log("Entered Player Turn State");
    }

    public override void OnExit()
    {
        //Debug.Log("Exited Player Turn State");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("MouseButton!");
            Tile hitTile = GetTileFromRaycast();
            Debug.Log(hitTile.Unit.name + "= unit name");
            Debug.Log(hitTile.Unit.CurrentTile + "= unit's current tile");
            Debug.Log(hitTile.Unit.transform.position + "= unit position");
            Debug.Log(hitTile.Position + "= hit Position");
            Debug.Log(hitTile.charges + "= hit charges");
        }

        if (Input.GetButtonDown("reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Try Moving
        if (Input.GetMouseButtonDown(0))
        {
            thereIsANextTile = true;

            Tile hitTile = GetTileFromRaycast();
            Tile playerTile = GameController.Instance.Level.GetPlayerTile();
            if (playerTile.Unit.moving) { return; }
            if (hitTile != null && playerTile != hitTile)
            {
                Tile curTile = playerTile;
                
                if (Vector3.Distance(curTile.Position, hitTile.Position) > 1f) { return; }
                direction = GetDirection(curTile.Position, hitTile.Position);

                while (curTile != null && curTile.Unit != null && thereIsANextTile)
                {
                    if(curTile.Unit.type == Unit.Type.Block) { move.Clear(); return; }

                    if(curTile.Unit.type == Unit.Type.Player)
                    {

                    }
                    else
                    {
                        Debug.Log("current tile step unit pos: " + curTile.Unit.transform.position + " and name: " + curTile.Unit.name);
                    }
                    move.Add(curTile.Unit);

                    if (GetNextTileInDirection(hitTile, direction) == null)
                    {
                        if (hitTile.charges == 2 || hitTile.charges == 3)
                        {
                            hitTile.moveDirection = direction;
                            hitTile.SpawnATile();
                            //hitTile.charges--;
                            //hitTile.GetComponent<MeshRenderer>().material = GameController.Instance.Colors.UpdateMaterialForTile(hitTile.charges);
                            hitTile.moveDirection = Vector3Int.zero;
                        }
                    }

                    if (GetNextTileInDirection(curTile, direction)==null)
                    {
                        thereIsANextTile = false;
                    }
                    else
                    {
                        curTile = GetNextTileInDirection(curTile, direction);
                    }
                    
                }
            }
        }

        if (move.Count == 0) { return; }

        for (int i = move.Count - 1; i >= 0; i--)
        {
            if(move[i].type == Unit.Type.Particle)
            {
                Particles u = move[i].GetComponent<Particles>();
                u.MoveInDirection(direction);
            }
            else
            {
                move[i].MoveInDirection(direction);
            }
        }
        move.Clear();

        GameController.Instance.EndPlayerTurn();
    }

    public Vector3Int GetDirection(Vector3Int start, Vector3Int end)
    {
        Vector3Int direction = end - start;

        return direction;
    }

    public Tile GetTileFromRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, tileLayer))
        {
            Tile tile = hit.collider.GetComponentInParent<Tile>();
            if (tile != null)
            {
                return tile;
            }
        }
        return null;
    }

    public void MoveUnitToTile(Tile current, Tile t, bool isParticle)
    {
        if (isParticle)
        {
            Particles p = current.Unit.GetComponent<Particles>();
            p.MoveToTile(t);
        }
        else
        {
            Player p = current.Unit.GetComponent<Player>();
            p.MoveToTile(t);
        }
    }

    public Tile GetNextTileInDirection(Tile t, Vector3Int dir)
    {
        if (GameController.Instance.Level.GetTileForPosition(t.Position + dir) != null)
        {
            return GameController.Instance.Level.GetTileForPosition(t.Position + dir);
        }
        else
            return null;

    }
}
