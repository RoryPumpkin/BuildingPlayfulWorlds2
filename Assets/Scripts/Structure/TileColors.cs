using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColors : MonoBehaviour
{
    public Material[] tileColors;

    public Material UpdateMaterialForTile(int charge)
    {
        if (charge == 9)
        {
            return tileColors[0];
        }
        else
        {
            if(charge <= 0)
            {
                return tileColors[1];
            }
            else
            {
                return tileColors[charge];
            }
        }
    }
}
