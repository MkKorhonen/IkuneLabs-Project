using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        //Prevents this gameObject from being destroyd when new scene is loaded.
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GhostData");

        if (objs.Length > 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
