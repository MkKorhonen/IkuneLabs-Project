using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostList : MonoBehaviour
{
    
    TextRay textRay;
    private List<GameObject> ghostCol = new List<GameObject>();
    List<int> ghostColor = new List<int>(4);

    public void CollectedGhost(GameObject ghost)
    {
        ghostCol.Add(ghost);
    }

    public void GetGhostColor(int col)
    {
        ghostColor.Add(col);
    }

    public List<int> GiveGhostColor()
    {
        return ghostColor;  
    }

}
