using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class WelcomePanel : MonoBehaviour
{
    public GameObject welcomePanel;
    public TextMeshProUGUI welcomeText;
    GhostList ghostList;
    int test;
    private int pageNum;

    private void Start()
    {
        ghostList = GameObject.FindGameObjectWithTag("GhostData").GetComponent<GhostList>();
        test = ghostList.FistTime();
        if (test > 1)
            welcomePanel.SetActive(false);
        welcomeText.text = "WELCOME TO PLAY Legend of Alummus!";
        pageNum = 1;
    }

    private void Update()
    {
        if (welcomePanel.activeSelf == true)
            PanelText();
        else
            return;
    }

    void PanelText()
    {
        if (Input.touchCount == 1 && welcomePanel.activeSelf == true)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {

                if (pageNum == 1)
                {
                    welcomeText.text = "The objective of this AR game is to purifie the unversity of Oulu from ghost of past professors!";
                }

                if (pageNum == 2)
                {
                    welcomeText.text = "In order to find ghost just roam around the campus map and if the ghost catches you, you will have a chance to catch it!";
                }

                if (pageNum == 3)
                {
                    welcomeText.text = "Tap the screen to start the catching tutorial!";
                }
                if (pageNum == 4)
                {
                    SceneManager.LoadScene("CatchingTutorial");
                }
                pageNum++;
            }

        }
    }
}
