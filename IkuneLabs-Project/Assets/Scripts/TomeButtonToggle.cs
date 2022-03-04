using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomeButtonToggle : MonoBehaviour
{
    public GameObject TomeOfGhosts;
    public void OpenTomeOfGhosts()
    {
        if(TomeOfGhosts != null)
        {
            bool isActive = TomeOfGhosts.activeSelf;
            TomeOfGhosts.SetActive(!isActive);
        }
    }
}
