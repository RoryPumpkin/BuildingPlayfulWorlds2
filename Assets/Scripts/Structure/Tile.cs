using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public Unit Unit { get; private set; }
    public Vector3Int Position { get; private set; }
    public Color neutralColor { get; private set; }

    //9 is unlimited; 0 is destroy; 1,2,3,4 are the respectable values
    public int charges;

    public Vector3Int moveDirection;

    public UnityEvent OnInitializedEvent;
    public UnityEvent SpawnTile;
    private Material mat;
    private Color baseColor;
    
    private void Awake()
    {
        
    }

    public void Init()
    {
        if (charges == 9 && GetComponent<MeshRenderer>().material.name != "Tile_Trigger_mat (Instance)")
        {
            GetComponent<MeshRenderer>().material = GameController.Instance.Colors.UpdateMaterialForTile(charges);
        }
        mat = GetComponent<MeshRenderer>().material;
        baseColor = mat.color;
        gameObject.SetActive(true);
        
        Unit = null;
        OnInitializedEvent?.Invoke();
    }
    public void OnMouseEnter()
    {
        mat.SetColor("_Color", new Color(mat.color.r+0.2f, mat.color.g+0.2f, mat.color.b+0.2f));
    }

    public void OnMouseExit()
    {
        mat.color = baseColor;
    }

    public void SpawnATile()
    {
        SpawnTile?.Invoke();
    }

    public void SetPosition(Vector3Int pos)
    {
        Position = pos;
        transform.position = pos;
    }

    public bool IsFree()
    {
        return Unit == null;
    }

    public void AssignUnit(Unit unit)
    {
        this.Unit = unit;
        if(unit.type == Unit.Type.Player && charges != 9)
        {
            charges--;

            if (charges > 0)
            {
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<BlockBreak>())
                    {
                        c.GetComponent<MeshRenderer>().material = GameController.Instance.Colors.UpdateMaterialForTile(charges);
                    }

                }
            }

            //transform.localScale *= 0.8f;
            /*
            if (GetComponent<CrackChilds>())
            {
                CrackChilds cc = GetComponent<CrackChilds>();
                cc.GetChildren();
                cc.crack = true;
                StartCoroutine(CrackTheChildren());
            }
            */
        }
    }

    public void ReleaseUnit()
    {
        Unit lastUnit = Unit;
        Unit = null;
        if (charges > 0 && lastUnit.GetComponent<Player>())
        {
            //charges--;
            /*
            foreach (Transform c in transform)
            {
                if (c.GetComponent<BlockBreak>())
                {
                    c.GetComponent<MeshRenderer>().material = GameController.Instance.Colors.UpdateMaterialForTile(charges);
                }
            }
            */
            //transform.localScale *= 0.8f;

            /*
            if (GetComponent<CrackChilds>() && charges > 0)
            {
                CrackChilds cc = GetComponent<CrackChilds>();
                cc.crack = true;
                StartCoroutine(UnCrackTheChildren());
                cc.UnCrack();
            }
            */
        }
        if(charges == 0)
        {
            GetComponentInChildren<MoveUpSlow>().enabled = true;
            GameController.Instance.Level.grid.Remove(Position);
        }
    }

    public Tile[] GetNeighbours()
    {
        return GameController.Instance.Level.grid.Values.ToList().FindAll(x => Vector3Int.Distance(Position, x.Position) == 1).ToArray();
    }

    public IEnumerator CrackTheChildren()
    {
        CrackChilds cc = GetComponent<CrackChilds>();
        while (Unit && cc.crack)
        {
            if (!Unit.GetComponent<Player>()) { yield return null; }
            cc.ChildCrack();

            yield return new WaitForSeconds(0.01f);
        }

        //cc.childs.Clear();
        yield return null;
    }

    public IEnumerator UnCrackTheChildren()
    {
        CrackChilds cc = GetComponent<CrackChilds>();
        while (Unit && cc.crack)
        {
            if (!Unit.GetComponent<Player>()) { yield return null; }
            cc.UnCrack();

            yield return new WaitForSeconds(0.01f);
        }

        //cc.childs.Clear();
        yield return null;
    }
}
