using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMenuButtoms : MonoBehaviour
{
    TextRay textRay;
    GhostScript ghostScript;

    public void StopGhost()
    {
        //Calls ChangeMovement function from GhostScript script
        textRay = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextRay>();
        ghostScript = GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostScript>();
        ghostScript.ChangeMovement();
        textRay.RemoveItemsFromInv(2);
    }

    public void DatabaseBut()
    {
        //Loads new scene
        SceneManager.LoadScene("GhostDatabase");
    }
}
