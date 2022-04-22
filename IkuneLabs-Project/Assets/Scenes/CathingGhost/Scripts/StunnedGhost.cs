using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StunnedGhost : MonoBehaviour
{
    public List<GameObject> stunnedPrefabs;
    public List<string> items;
    public GameObject catchingScreen;
    private GameObject stunnedGhost;
    public TextMeshProUGUI errorText;
    GhostList ghostList;
    int ghostColl;
    public GameObject capsule;
    public GameObject itemMenu;

    TextRay textRay;

    public void StunnedGhosts(Vector3 pos, int ghostCol)
    {
        //Sets catchingScreen object active and spawns new stunnedghost prefab when this function is called
        catchingScreen.SetActive(true);
        ghostColl = ghostCol;

        stunnedGhost = Instantiate(stunnedPrefabs[ghostCol], pos, Quaternion.identity);
        stunnedGhost.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
    }

    public void CatchButtom()
    {
        //Check if CatchingScreen is active and if it is not is shows errors text,
        //if the catchingScreen is active it will deactivate itemMenu gameobject and set capsule gameobject active
        if (catchingScreen.activeSelf == true)
        {
            itemMenu.SetActive(false);
            capsule.SetActive(true);
            textRay.RemoveItemsFromInv(1);
            
        }
        else
        {
            errorText.text = "Can't capture ghost that is not stunned!";
            StartCoroutine(ClearErrorText());
            return;
        }
    }

    IEnumerator ClearErrorText()
    {
        //clears error text after 2 second delay
        yield return new WaitForSeconds(2);
        errorText.text = "";
    }

    public void GhostCaptured()
    {
        //Calls GetGhostColor function, CatchGhost function and destroys stunnedGhost gameobject
        textRay = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextRay>();
        ghostList = GameObject.FindGameObjectWithTag("GhostData").GetComponent<GhostList>();

        ghostList.GetGhostColor(ghostColl);
        textRay.LootHandler(ghostColl);
        textRay.CatchGhost(stunnedGhost);
        Destroy(stunnedGhost);
    }
}
