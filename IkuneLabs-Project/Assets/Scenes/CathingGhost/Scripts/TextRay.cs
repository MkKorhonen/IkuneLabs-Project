using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TextRay : MonoBehaviour
{

    private List<GameObject> ghost = new List<GameObject>();
    private GameObject curSelected;
    private Camera cam;
    public List<GameObject> ghostPrefabs;
    public float spawnDistance;
    public GameObject selector;
    private int ghostCol;
    public GameObject itemMenu;
    public GameObject gun;

    public float travelTime;

    private int health;
    public TextMeshProUGUI healthText;
    public int pHealth = 3;
    public TextMeshProUGUI phealthText;
    public Slider slider;
    public TextMeshProUGUI errorText;

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
    public float attackRange;
    GhostScript ghostScript;
    StunnedGhost stunnedGhost;

    public static TextRay instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Setting some starting values

        cam = Camera.main;
        SpawnGhost();
        errorText.text = "";
        phealthText.text = "Player HP: " + pHealth;
        curCatchTime = catchTime;
        ghostScript = GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostScript>();
        stunnedGhost = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StunnedGhost>();
    }

    private void Update()
    {
        //Makes the ghost prefab always look at the AR camera when player is rotatian around the ghost
        ghost[0].transform.rotation = Quaternion.LookRotation(-cam.transform.forward, cam.transform.up);

        //stops the game when gameover function is called
        if (gameOver)
            return;

        //Updates the timer the player has to catch the ghost
        timerText.text = "Time to Catch: " + Mathf.RoundToInt(curCatchTime) + "s" + "!";
        curCatchTime -= Time.deltaTime;

        if(curCatchTime <= 0)
        {
            MissGhost();
        }

        //Shoots ray from the cross hair when player hold her/his finger on the screen, if it hits an object that is in the spawned ghost list it will select it
        if (Input.touchCount > 0 && itemMenu.activeSelf == false)
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
    public void SpawnGhost()
    {

        int _num = 0;

        foreach (GameObject gameObject in ghostPrefabs)
        {
            _num++;
        }
        int randomNumber = Random.Range(0, _num);

        /*Vector3 spawnCircle = Random.onUnitSphere;
        spawnCircle.y = -1;

        Vector3 spawnPos = cam.transform.position + (spawnCircle * spawnDistance);*/


        GameObject obj = Instantiate(ghostPrefabs[randomNumber], new Vector3(0, -1, spawnDistance), Quaternion.identity);
        ghostCol = randomNumber;
        ghost.Add(obj);
    }

    //Makes the healthbar move everytime this function is called and if the health goes to zero destroy the ghost prefab and open new window
   public void TakeDamage()
    {
        health--;
        slider.value--;

        healthText.text = "Hp: " + health;
        GhostStatus(health);


       if (health <= 0)
       {
            //if health is zero, deactivate and destroys most of the gameobjects and calls StunnedGhost function
            Vector3 pos = ghost[0].transform.position;
            ghost.Remove(curSelected);
            Destroy(curSelected);
            timerText.enabled = false;
            phealthText.enabled = false;
            gun.SetActive(false);
            stunnedGhost.StunnedGhosts(pos, ghostCol);
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
        TakeDamage();
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
    public void GetHealth(int hp)
    {
        health = hp;
        healthText.text = "HP: " + health;
        slider.maxValue = health;
        slider.value = health;
    }

    //Checks the ghost prefabs hp status and calls moveghost function if the ghost's hp is too low 
    void GhostStatus(int shp)
    {
        ghostScript.HpStatus(shp);
    }

    //adds more catchTime
    public void MoreTime()
    {
        curCatchTime += 30;
    }

    //Activate armor gameobject
    public void ActiveArmor()
    {
        armor.SetActive(true);
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
}
