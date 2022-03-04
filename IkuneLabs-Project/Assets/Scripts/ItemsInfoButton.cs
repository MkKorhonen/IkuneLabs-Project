using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInfoButton : MonoBehaviour
{
    public GameObject ButtonItems;
    public void OpenButtonItems()
    {
        if(ButtonItems != null)
        {
            bool isActive = ButtonItems.activeSelf;
            ButtonItems.SetActive(!isActive);
        }
    }
}
