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
        ghostScript = GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostScript>();
        ghostScript.ChangeMovement();
    }

    public void DatabaseBut()
    {
        //Loads new scene
        SceneManager.LoadScene("GhostDatabase");
    }

    public void RunButtom()
    {
        SceneManager.LoadScene("CampusScene");
    }
}
