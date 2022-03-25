using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GhostScript : MonoBehaviour
{
    public int health;
    private Camera cam;
    TextRay textRay;
    public int movementPattern;
    StunnedGhost stunnedGhost;
    private int stopMoving;
    private int curHealth;
    private int statHealth;
    int lastnum = 2;

    public float moveSpeed;
    private Vector3 player;
    public float width;
    public float height;
    private float timeC;

    public float attackRange;

    private int directionValue;
    public float travelTime;
    private float lastTime;

    public TextMeshProUGUI nameText;
    public string ghostName;


    void Start()
    {
        textRay = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextRay>();
        textRay.GetHealth(health);
        cam = Camera.main;
        lastTime = Time.time;
        directionValue = 1;
        stopMoving = 1;
        GiveGhostName();
        statHealth = health;
    }

    void Update()
    {
        //Updates the players mosition and rotates the nameText to always look at the player.
        //Calls the 4 differend kind of movement functios based on given movePattern value
        player = cam.transform.position;
        player.y = -3;
        nameText.transform.rotation = Quaternion.LookRotation(nameText.transform.position - cam.transform.position);

        if (movementPattern == 1)
            CircleMovement();

        if (movementPattern == 2)
            GhostAttack();

        if (movementPattern == 3)
            SidewayMovement();

        if (movementPattern == 4)
            TeleportMovement();
    }

    public void CircleMovement()
    {
        //If stopMoving value is not 1 call StopGhost function if it is 1 moves the ghost gameobject in an oval shape
        if (stopMoving == 1)
        {
            timeC += Time.deltaTime * moveSpeed;
            float x = Mathf.Cos(timeC) * width;
            float y = Mathf.Sin(timeC) * height;

            //transform.LookAt(player);
            transform.position = new Vector3(x, y, 5);
        }
        else
            StartCoroutine(StopGhost());
    }

    public void GhostAttack()
    {
        //If stopMoving value is not 1 call StopGhost function if it is 1 moves to ghost towards the player as long as it is not in attack range
        //when it is in attack range call PlayerTakeDamage function and move the ghost back 5 units
        if (stopMoving == 1)
        {
            float dist = Vector3.Distance(transform.position, player);

            if (dist > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player, moveSpeed * Time.deltaTime);
            }
            else
            {
                textRay.PlayerTakeDamage();
                Vector3 test = transform.position;
                if (test.z > 0)
                    transform.position += new Vector3(0, 0, 5);
                else
                    transform.position += new Vector3(0, 0, -5);
            }

            transform.LookAt(player);
        }
        else
            StartCoroutine(StopGhost());
    }

    public void SidewayMovement()
    {
        //If stopMoving value is not 1 call StopGhost function if it is 1 loops the ghost right for travelTime value and then moves the ghost
        //left for travelTime value
        if (stopMoving == 1)
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
                    transform.LookAt(player);
                    transform.position += transform.right * moveSpeed * Time.deltaTime;
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
                    transform.LookAt(player);
                    transform.position += -transform.right * moveSpeed * Time.deltaTime;
                }
            }
        }
        else
            StartCoroutine(StopGhost());
    }

    public void TeleportMovement()
    {
        //positions the ghost back to its orginal postion and the rolls random number between 1,2,3 and move the ghost to right, left or up 3 units
        //depending on whitch number got rolled
        if(statHealth - curHealth == 50)
        {
            transform.position = new Vector3(0, -1, 6);
            int num = Random.Range(1, 4);
            
            while (lastnum == num)
            {
                num = Random.Range(1, 4);
            }
            Vector3 curPosi = transform.position;

            if(num == 1)
            {
                transform.position += transform.right * 3;
                lastnum = 1;
            }

            if(num == 2)
            {
                transform.position += -transform.right * 3;
                lastnum = 2;
            }

            if (num == 3)
            {
                transform.position += transform.up * 3;
                lastnum = 3;
            }

            statHealth = curHealth;
        }
    }

    public void HpStatus(int hp)
    {
        curHealth = hp;
    }

    IEnumerator StopGhost()
    {
        yield return new WaitForSeconds(5);
        stopMoving = 1;
    }

    public void ChangeMovement()
    {
        stopMoving++;
    }

    void GiveGhostName()
    {
        //changes the nameText text
        int level = Random.Range(1, 6);
        nameText.text = ghostName + " " + "lvl: " + level;
    }
}
