using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsInfoButton : MonoBehaviour
{
    public GameObject ButtonGhosts;
    public void OpenButtonGhosts()
    {
        if(ButtonGhosts != null)
        {
            bool isActive = ButtonGhosts.activeSelf;
            ButtonGhosts.SetActive(!isActive);
        }
    }
}