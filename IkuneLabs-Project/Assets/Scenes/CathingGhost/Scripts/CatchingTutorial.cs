using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CatchingTutorial : MonoBehaviour
{

    private List<GameObject> ghost = new List<GameObject>();
    private GameObject curSelected;
    private Camera cam;
    public List<GameObject> ghostPrefabs;
    public float spawnDistance;
    public GameObject selector;
    public GameObject itemMenu;
    public GameObject gun;
    public int ghostHealth;

    private int directionValue;
    public float travelTime;
    private float lastTime;
    private Vector3 player;

    public GameObject catchingScreen;
    public GameObject stunnedPrefabs;
    private GameObject stunnedGhost;

    private int health;
    public TextMeshProUGUI healthText;
    public int pHealth = 3;
    public TextMeshProUGUI phealthText;
    public Slider slider;
    public TextMeshProUGUI errorText;
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    private int pageNum;

    public TextMeshProUGUI timerText;
    public float catchTime;
    private float curCatchTime;

    public bool gameOver;
    public GameObject missghostScreen;
    public GameObject catchScreen;
    public GameObject deathScreen;
    public GameObject armor;

    public float moveSpeed;

    public GhostList ghostList;
    GhostScript ghostScript;

    public GameObject capsule;
    public GameObject itemMenuButtom;
    public GameObject shader;
    public GameObject canvas;
    GameObject arrow;
    int x;

    public static CatchingTutorial instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Setting some starting values
        gun.SetActive(false);
        pageNum = 1;
        directionValue = 1;
        tutorialText.text = "Hey you have encountered a ghost!";
        cam = Camera.main;
        SpawnGhost(ghostPrefabs[0]);
        errorText.text = "";
        phealthText.text = "Player HP: " + pHealth;
        curCatchTime = catchTime;
        GetHealth();
        ghostScript = GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostScript>();
    }

    private void Update()
    {
        //Makes the ghost prefab always look at the AR camera when player is rotatian around the ghost
        foreach (GameObject y in ghost)
        { 
            x++;
            if (x > 0)
                ghost[0].transform.rotation = Quaternion.LookRotation(-cam.transform.forward, cam.transform.up);
            x = 0;
        }

        if (tutorialPanel.activeSelf == false)
        {
            timerText.text = "Time to Catch: " + Mathf.RoundToInt(curCatchTime) + "s" + "!";
            curCatchTime -= Time.deltaTime;
        }

        TutorialText();

        //stops the game when gameover function is called
        if (gameOver)
            return;

        //Updates the timer the player has to catch the ghost
        if (tutorialPanel.activeSelf == false && ghostHealth > 0)
        { 
            player = cam.transform.position;
            player.y = -3;
            MoveGhost();
        }

        if (curCatchTime <= 0)
        {
            MissGhost();
        }

        //Shoots ray from the cross hair when player hold her/his finger on the screen, if it hits an object that is in the spawned ghost list it will select it
        if (Input.touchCount > 0 && itemMenu.activeSelf == false && tutorialPanel.activeSelf == false && ghostHealth > 0)
        {
            Ray ray = cam.ScreenPointToRay(selector.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null && ghost.Contains(hit.collider.gameObject))
                {
                    TakeDamage();
                    if (curSelected != null && hit.collider.gameObject != curSelected)
                        Select(hit.collider.gameObject);
                    else if (curSelected == null)
                        Select(hit.collider.gameObject);
                }
            }
            else
            {
                Deselect();
            }

        }
        else
            Deselect();
    }

    //Changes the ghost prefabs visuals when it is selected or deselected
    void Select(GameObject selected)
    {

        if (curSelected != null)
            ToggleSelectionVisual(curSelected, true);

        curSelected = selected;
        ToggleSelectionVisual(curSelected, false);

    }

    void Deselect()
    {
        if (curSelected != null)
            ToggleSelectionVisual(curSelected, true);

        curSelected = null;
    }

    void ToggleSelectionVisual(GameObject obj, bool toggle)
    {
        obj.transform.Find("NormalGhost").gameObject.SetActive(toggle);
    }

    public void ResetTest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Scenechange()
    {

    }

    //Spawns a random ghost prefab and then adds it in the list of spawned ghosts
    public void SpawnGhost(GameObject obj)
    {

        /*Vector3 spawnCircle = Random.onUnitSphere;
        spawnCircle.y = -1;

        Vector3 spawnPos = cam.transform.position + (spawnCircle * spawnDistance);*/


        GameObject ghostObj = Instantiate(obj, new Vector3(0, -1, spawnDistance), Quaternion.identity);
        ghost.Add(ghostObj);
    }

    //Makes the healthbar move everytime this function is called and if the health goes to zero destroy the ghost prefab and open new window
    public void TakeDamage()
    {
        ghostHealth--;
        slider.value--;

        healthText.text = "Hp: " + ghostHealth;

        if (ghostHealth <= 0)
        {
            //if health is zero, deactivate and destroys most of the gameobjects and calls StunnedGhost function
            //Vector3 pos = ghost[0].transform.position;
            Destroy(ghost[0]);
            ghost.Remove(ghost[0]);
            SpawnGhost(ghostPrefabs[1]);
            timerText.enabled = false;
            phealthText.enabled = false;
            gun.SetActive(false);
            tutorialPanel.SetActive(true);
        }



    }

    //If armor gameObject is not active minus 1 from playerHealth text.
    //if health goes to zero call deathScreen function
    public void PlayerTakeDamage()
    {
        if (armor.activeSelf == false)
        {
            pHealth--;
            phealthText.text = "Player HP: " + pHealth;

            if (pHealth <= 0)
            {
                DeathScreen();
            }
        }
        else return;

    }

    public void TestButtom()
    {
        StartCoroutine(SmallDelay());
    }

    public void TestButtom1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Stops the game, destroys the current selected ghost prefab and opens new window
    void MissGhost()
    {
        gameOver = true;
        ghost.Remove(curSelected);
        Destroy(curSelected);
        missghostScreen.SetActive(true);

        StartCoroutine(ScenechangeDelay(missghostScreen));

    }

    //Stops the game, destroys the current selected ghost prefab and opens new window
    public void CatchGhost(GameObject obj)
    {
        gameOver = true;
        catchScreen.SetActive(true);
        ghostList.CollectedGhost(obj);

        StartCoroutine(ScenechangeDelay(catchScreen));
    }

    //Stops the game, destroys the current selected ghost prefab and opens new window
    void DeathScreen()
    {
        gameOver = true;
        ghost.Remove(curSelected);
        Destroy(curSelected);
        deathScreen.SetActive(true);

        StartCoroutine(ScenechangeDelay(deathScreen));

    }

    IEnumerator ScenechangeDelay(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        obj.SetActive(false);
    }

    //sets health and healthText with given value
    public void GetHealth()
    {
        healthText.text = "HP: " + ghostHealth;
        slider.maxValue = ghostHealth;
        slider.value = ghostHealth;
    }

    //activates and deactivates itemMenu gameobject
    public void ItemMenuButtom()
    {
        if (itemMenu.activeSelf == false)
        {
            itemMenu.SetActive(true);
            gun.SetActive(false);
        }
        else
        {
            itemMenu.SetActive(false);
            gun.SetActive(true);
        }
    }

    void TutorialText()
    {
        if(Input.touchCount == 1 && tutorialPanel.activeSelf == true)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {

                if (pageNum == 1)
                {
                    tutorialText.text = "In order to to capture the ghost you need to stun it first";
                }

                if (pageNum == 2)
                {
                    tutorialText.text = "To stun the ghost you need to drain its energy to 0 using the plasma gun!";
                }

                if (pageNum == 3)
                {
                    tutorialText.text = "To use the plasma gun just hold down your finger and point the crosshair to the ghost and when its energy goes " +
                        "to zero it will get stunned and you have change to capture the ghost";
                }
                if (pageNum == 4)
                {
                    arrow = canvas.transform.Find("Arrow1").gameObject;
                    arrow.SetActive(true);
                    tutorialText.text = "On top side of the screen you can see how much energy the ghost has and under it you can see the timer " +
                        "that tells you how much time you have before the ghost flees";
                }
                if (pageNum == 5)
                {
                    arrow.SetActive(false);
                    tutorialText.text = "Now try to drain the ghost's energy by shooting it with the plasma gun!";
                }
                if (pageNum == 6)
                {
                    tutorialText.text = "Good job! Now the ghost is stunned and ready to be captured";
                    tutorialPanel.SetActive(false);
                    gun.SetActive(true);
                }

                if (pageNum == 7)
                {
                    tutorialText.text = "To capture the ghost press the bag buttom at bottom of the screen to see your items";
                    arrow = canvas.transform.Find("Arrow3").gameObject;
                    arrow.SetActive(true);
                    shader.transform.position = new Vector2(4, 202);
                    itemMenuButtom.SetActive(true);
                    while (itemMenu.activeSelf == false)
                    {
                        return;
                    }
                }

                if (capsule.activeSelf == true)
                {
                    tutorialPanel.SetActive(false);
                    arrow = canvas.transform.Find("Arrow3").gameObject;
                    arrow.SetActive(false);

                }

                if (pageNum == 9)
                {
                    tutorialText.text = "Now you are ready for capturing more ghosts! ";
                        
                }

                if (pageNum == 10)
                {
                    tutorialText.text = "There are many different kind of ghosts you can catch and some of them can attack you so be carefull!" +
                        " Some of them can even drop items so make sure to check new items from your bag!";
                }

                if (pageNum == 11)
                {
                    tutorialText.text = "Tap the screen to start exploring the Campus!";
                }

                if (pageNum == 12)
                {
                    SceneManager.LoadScene("CampusScene");
                }
                pageNum++;
            }
            
        }

    }

    void MoveGhost()
    {
        if (directionValue == 1)
        {
            if (Time.time - lastTime > travelTime)
            {
                lastTime = Time.time;
                directionValue++;
            }
            else
            {
                ghost[0].transform.LookAt(player);
                ghost[0].transform.position += ghost[0].transform.right * moveSpeed * Time.deltaTime;
            }

        }


        if (directionValue == 2)
        {
            if (Time.time - lastTime > travelTime)
            {
                lastTime = Time.time;
                directionValue--;
            }
            else
            {
                ghost[0].transform.LookAt(player);
                ghost[0].transform.position += -ghost[0].transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }

    public void CatchButtom()
    {
        arrow = canvas.transform.Find("Arrow2").gameObject;
        arrow.SetActive(false);
        itemMenu.SetActive(false);
        capsule.SetActive(true);
        tutorialText.text = "Now to throw the capture device just tap the screen and point at the ghost with the crosshair "
        + "if you miss just try again";
    }

    public void ShowItemMenu()
    {
        arrow.SetActive(false);
        itemMenu.SetActive(true);
        arrow = canvas.transform.Find("Arrow2").gameObject;
        arrow.SetActive(true);
        tutorialText.text = "Now tap the capture device from the item menu";
    }

    public void GhostCaptured()
    {
        Destroy(ghost[0]);
        ghost.Remove(ghost[0]);
        catchScreen.SetActive(true);
        StartCoroutine(SmallDelay());
    }

    IEnumerator SmallDelay()
    {
        yield return new WaitForSeconds(3);
        capsule.SetActive(false);
        catchScreen.SetActive(false);
        tutorialPanel.SetActive(true);
        tutorialText.text = "Great job! You have captured the ghost!";
    }

}
