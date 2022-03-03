using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextRay : MonoBehaviour
{

    private List<GameObject> ghost = new List<GameObject>();
    private GameObject curSelected;
    private Camera cam;
    public GameObject ghostPrefab;
    public float spawnDistance;
    public GameObject selector;

    public float travelTime;
    private float lastTime;
    private int directionValue;
    private float phase1;
    
    public int health = 200;
    public TextMeshProUGUI healthText;
    
    public TextMeshProUGUI timerText;
    public float catchTime;
    private float curCatchTime;

    public Slider slider;

    public bool gameOver;
    public GameObject missghostScreen;
    public GameObject catchScreen;

    public float moveSpeed;

    public static TextRay instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Setting some starting values

        healthText.text = "HP: " + health;
        slider.maxValue = health;
        slider.value = health;
        curCatchTime = catchTime;
        cam = Camera.main;
        SpawnGhost();
        lastTime = Time.time;
        directionValue = 1;
        phase1 = health / 2;
    }

    private void Update()
    {
        //Makes the ghost prefab always look at the AR camera when player is rotatian around the ghost
        ghost[0].transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);

        GhostStatus();


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
        if (Input.touchCount > 0)
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
            ToggleSelectionVisual(curSelected, false);

        curSelected = selected;
        ToggleSelectionVisual(curSelected, true);

    }

    void Deselect()
    {
        if (curSelected != null)
            ToggleSelectionVisual(curSelected, false);

        curSelected = null;
    }

    void ToggleSelectionVisual(GameObject obj, bool toggle)
    {
        obj.transform.Find("DamagedGhost").gameObject.SetActive(toggle);
    }

    public void ResetTest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Scenechange()
    {
        SceneManager.LoadScene("CampusScene");
    }

    //Spawns the ghost prefab and then adds it in the list of spawned ghosts
    void SpawnGhost()
    {

        Vector3 spawnCircle = Random.onUnitSphere;
        spawnCircle.y = -1;

        Vector3 spawnPos = cam.transform.position + (spawnCircle * spawnDistance);

        GameObject obj = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
        ghost.Add(obj);
    }

    //Makes the healthbar move everytime this function is called and if the health goes to zero destroy the ghost prefab and open new window
   void TakeDamage()
    {
        health--;
        slider.value--;

        healthText.text = "Hp: " + health;
        

        if(health <= 0)
        {
            ghost.Remove(curSelected);
            Destroy(curSelected);
            CatchGhost();
        }

    }

    //Stops the game, destroys the current selected ghost prefab and opens new window
    void MissGhost()
    {
        gameOver = true;
        ghost.Remove(curSelected);
        Destroy(curSelected);
        missghostScreen.SetActive(true);

        StartCoroutine(ScenechangeDelay());

    }

    public void CatchGhost()
    {
        gameOver = true;
        catchScreen.SetActive(true);

        StartCoroutine(ScenechangeDelay());
    }

    IEnumerator ScenechangeDelay()
    {
        yield return new WaitForSeconds(3);
        Scenechange();
    }

    //Checks the ghost prefabs hp status and calls moveghost function if the ghost's hp is too low 
    void GhostStatus()
    {
        if(health < phase1)
        {
            MoveGhost();
        }
    }

    //Moves the ghost around the player camare in semi cirle motion back and forth
    public void MoveGhost()
    {
        if(directionValue == 1)
        {
            if (Time.time - lastTime > travelTime)
            {
                lastTime = Time.time;
                directionValue++;
            }
            else
            {
                ghost[0].transform.LookAt(cam.transform.position);
                ghost[0].transform.position += ghost[0].transform.right * moveSpeed * Time.deltaTime;
            }

        }
        

        if(directionValue == 2)
        {
            if (Time.time - lastTime > travelTime)
            {
                lastTime = Time.time;
                directionValue--;
            }
            else
            {
                ghost[0].transform.LookAt(cam.transform.position);
                ghost[0].transform.position += -ghost[0].transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }

}
