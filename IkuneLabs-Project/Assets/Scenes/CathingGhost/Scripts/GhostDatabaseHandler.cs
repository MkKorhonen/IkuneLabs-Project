using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GhostDatabaseHandler : MonoBehaviour
{
    List<GameObject> ghostImages = new List<GameObject>();
    RawImage test;
    GameObject test2;
    public List<Texture> wgTexture;
    GhostList ghostList;
    List<int> col;
    public GameObject infoBox;
    public TextMeshProUGUI infoText;
    GhostInfo ghostInfo;

    private void Start()
    {
        //Adds every gameObject with tag "GhostIcon" to list then it calls a list of ghost id:s from Ghostlist script and then
        //gives the GhostIcon gameObjects new texture if they match with same id:s in ghostImages list.
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("GhostIcon"))
        {
            ghostImages.Add(gameObject);
        }
        ghostList = GameObject.FindGameObjectWithTag("GhostData").GetComponent<GhostList>();
        col = ghostList.GiveGhostColor();

        foreach (int x in col)
        {
            if (x == 4)
                return;
            test = ghostImages[x].GetComponent<RawImage>();
            test.texture = wgTexture[x];
        }
    }

    public void Exitbut()
    {
        //Loads new scene
        SceneManager.LoadScene("CampusScene");
    }

    public void WhiteInfoBox()
    {
        //Opens new gameobject and modifies the text in the gameobject
        ghostInfo = GameObject.FindGameObjectWithTag("GhostInfo").GetComponent<GhostInfo>();
        string texti = ghostInfo.GiveGhostInfo(0);
        infoBox.SetActive(true);
        infoText.text = texti;

    }

    public void GreenInfoBox()
    {
        //Opens new gameobject and modifies the text in the gameobject
        ghostInfo = GameObject.FindGameObjectWithTag("GhostInfo").GetComponent<GhostInfo>();
        string texti = ghostInfo.GiveGhostInfo(1);
        infoBox.SetActive(true);
        infoText.text = texti;

    }

    public void YellowInfoBox()
    {
        //Opens new gameobject and modifies the text in the gameobject
        ghostInfo = GameObject.FindGameObjectWithTag("GhostInfo").GetComponent<GhostInfo>();
        string texti = ghostInfo.GiveGhostInfo(2);
        infoBox.SetActive(true);
        infoText.text = texti;

    }

    public void BlueInfoBox()
    {
        //Opens new gameobject and modifies the text in the gameobject
        ghostInfo = GameObject.FindGameObjectWithTag("GhostInfo").GetComponent<GhostInfo>();
        string texti = ghostInfo.GiveGhostInfo(3);
        infoBox.SetActive(true);
        infoText.text = texti;

    }
}
