using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorData : MonoBehaviour
{
    public GameObject ghost;
    public string id = "";
    public int[] co2Levels = new int[7];
    public int currentCo2;
    public bool canSpawn = false;
    public int spawnInterval = 3;
    public int spawnCount = 1;
    public Text spawnText;
    private DateTime dt;
    private string currentDay;
    private Bounds bounds;
    private float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        // Get the co2 levels from the week and the current day
        dt = DateTime.Now;
        currentDay = dt.DayOfWeek.ToString();
        currentCo2 = GetCo2LevelForCurrentDay();
        bounds = GetComponent<Collider>().bounds;
        spawnText.enabled = false;
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnTimer()
    {
        while (spawnCount > 0)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (canSpawn)
            {
                spawnCount--;
                SpawnGhost();
            }
        }
    }

    // Maybe add interval change based on co2 level?
    private void SetSpawnCount()
    {
        // Set the amount of ghosts to spawn to correspond to co2 levels in the spawn area
        if(currentCo2 < 600)
        {
            spawnText.text = "Ghost activity: Very low";
            spawnCount = 1;
        }
        else if(currentCo2 < 700)
        {
            spawnText.text = "Ghost activity: Low";
            spawnCount = 2;
        }
        else if(currentCo2 < 800)
        {
            spawnText.text = "Ghost activity: Medium";
            spawnCount = 3;
        }
        else if (currentCo2 < 900)
        {
            spawnText.text = "Ghost activity: High";
            spawnCount = 4;
        }
        else
        {
            spawnText.text = "Ghost activity: Very high";
            spawnCount = 5;
        }
    }

    private int GetCo2LevelForCurrentDay()
    {
        int result = -100;
        switch(currentDay)
        {
            case "Monday":
                result = co2Levels[0];
                break;
            case "Tuesday":
                result = co2Levels[1];
                break;
            case "Wednesday":
                result = co2Levels[2];
                break;
            case "Thursday":
                result = co2Levels[3];
                break;
            case "Friday":
                result = co2Levels[4];
                break;
            case "Saturday":
                result = co2Levels[5];
                break;
            case "Sunday":
                result = co2Levels[6];
                break;
            default:
                result = 100;
                break;
        }
        return result;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canSpawn = true;
            SetSpawnCount();
            spawnText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canSpawn = false;
            spawnText.enabled = false;
        }
    }

    void SpawnGhost()
    {
        x = UnityEngine.Random.Range(-bounds.extents.x, bounds.extents.x);
        y = UnityEngine.Random.Range(-bounds.extents.y, bounds.extents.y);
        z = UnityEngine.Random.Range(-bounds.extents.z, bounds.extents.z);
        Instantiate(ghost, bounds.center + new Vector3(x, y, z), Quaternion.identity);
    }
}
