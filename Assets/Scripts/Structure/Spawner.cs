using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToSpawn = null;
    [SerializeField] private GameObject ParticleToSpawn = null;
    private Unit u;

    public void SpawnUnit()
    {
        Instantiate(ObjectToSpawn, transform.position + Vector3.up, transform.rotation);
    }

    public void SpawnTile()
    {
        GameObject tile = Instantiate(ObjectToSpawn, transform.position - Vector3.up, transform.rotation);
        GameController.Instance.Level.AddTile(Vector3Int.RoundToInt(transform.position - Vector3.up), tile.GetComponent<Tile>());
    }

    public void SpawnTileInsteadOfThis()
    {

        if(GameController.Instance.Level.GetTileForPosition(transform.position).Unit != null)
        {
            u = GameController.Instance.Level.GetTileForPosition(transform.position).Unit;
        }
        
        GameController.Instance.Level.grid.Remove(Vector3Int.RoundToInt(transform.position));
        gameObject.SetActive(false);
        GameObject tile = Instantiate(ObjectToSpawn, transform.position, transform.rotation);
        GameController.Instance.Level.AddTile(Vector3Int.RoundToInt(transform.position), tile.GetComponent<Tile>());
        if (GameController.Instance.Level.GetTileForPosition(transform.position).Unit != null)
        {
            tile.GetComponent<Tile>().AssignUnit(u);
        }
        
    }

    public void SpawnTileInDirection()
    {
        Vector3Int dir = GameController.Instance.Level.GetTileForPosition(transform.position).moveDirection;
        GameObject tile = Instantiate(ObjectToSpawn, transform.position + dir, transform.rotation);
        GameController.Instance.Level.AddTile(Vector3Int.RoundToInt(transform.position + dir), tile.GetComponent<Tile>());
    }

    public void SpawnTileInDirectionFromParticle()
    {
        Vector3Int dir = GameController.Instance.Level.GetTileForPosition(transform.position - Vector3.up).moveDirection;
        GameObject tile = Instantiate(ObjectToSpawn, transform.position + dir - Vector3.up, transform.rotation);
        GameController.Instance.Level.AddTile(Vector3Int.RoundToInt(transform.position + dir - Vector3.up), tile.GetComponent<Tile>());
    }

    public void SpawnTileInEveryDirection()
    {
        SpawnTile();

        for (int i = 0; i <= 4; i++)
        {
            if (i == 0)
            {
                Vector3 direction = Vector3.right;
                UpdateMoveDirection(direction);
            }
            else if (i == 1)
            {
                Vector3 direction = -Vector3.right;
                UpdateMoveDirection(direction);
            }
            else if (i == 3)
            {
                Vector3 direction = Vector3.forward;
                UpdateMoveDirection(direction);
            }
            else if(i == 4)
            {
                Vector3 direction = -Vector3.forward;
                UpdateMoveDirection(direction);
            }
        }
        
    }

    public void SpawnParticle()
    {
        GameObject p = Instantiate(ParticleToSpawn, transform.position, transform.rotation);
        GameController.Instance.Level.GetTileForPosition(p.gameObject.transform.position - Vector3.up).ReleaseUnit();
        p.GetComponent<Particles>().CurrentTile = GameController.Instance.Level.GetTileForPosition(p.gameObject.transform.position - Vector3.up);
        GameController.Instance.Level.GetTileForPosition(p.gameObject.transform.position - Vector3.up).AssignUnit(p.GetComponent<Unit>());
    }

    public void UpdateMoveDirection(Vector3 dir)
    {
        if (GameController.Instance.Level.GetTileForPosition(transform.position - Vector3.up + dir) == null)
        {
            Tile mid = GameController.Instance.Level.GetTileForPosition(transform.position - Vector3.up);
            mid.moveDirection = Vector3Int.RoundToInt(dir);
            SpawnTileInDirectionFromParticle();
        }
    }

    
}
