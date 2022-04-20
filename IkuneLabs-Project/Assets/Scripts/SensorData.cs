using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorData : MonoBehaviour
{
    public GameObject ghost;
    public string id = "";
    // 3 co2 levels per day every day of the week, from 12pm to 8am, 8am to 4pm, 4pm to 12pm
    public int[] co2Levels = new int[21];
    public int currentCo2;
    public bool canSpawn = false;
    public int spawnInterval = 3;
    public int spawnCount = 1;
    public Text spawnText;
    private DateTime dt;
    private string currentDay;
    private string currentTime;
    private Bounds bounds;
    private float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        // Get the co2 levels from the week and the current day
        dt = DateTime.Now;
        currentDay = dt.DayOfWeek.ToString();
        currentTime = dt.TimeOfDay.ToString();
        currentTime = currentTime.Substring(0, currentTime.IndexOf(":"));
        currentCo2 = GetCo2Level();
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
        if(currentCo2 < 500)
        {
            spawnText.text = "Ghost activity: Very low";
            spawnCount = 1;
        }
        else if(currentCo2 < 600)
        {
            spawnText.text = "Ghost activity: Low";
            spawnCount = 2;
        }
        else if(currentCo2 < 700)
        {
            spawnText.text = "Ghost activity: Medium";
            spawnCount = 3;
        }
        else if (currentCo2 < 800)
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

    private int GetCo2Level()
    {
        int result = 0;
        int time = int.Parse(currentTime);

        if(currentDay == "Monday")
        {
            if(time < 08)
            {
                result = co2Levels[0];
            }
            else if(time > 08 && time < 16)
            {
                result = co2Levels[1];
            }
            else
            {
                result = co2Levels[2];
            }
        }
        if (currentDay == "Tuesday")
        {
            if (time < 08)
            {
                result = co2Levels[3];
            }
            else if (time > 08 && time < 16)
            {
                result = co2Levels[4];
            }
            else
            {
                result = co2Levels[5];
            }
        }
        if (currentDay == "Wednesday")
        {
            if (time < 08)
            {
                result = co2Levels[6];
            }
            else if (time > 08 && time < 16)
            {
                result = co2Levels[7];
            }
            else
            {
                result = co2Levels[8];
            }
        }
        if (currentDay == "Thursday")
        {
            if (time < 08)
            {
                result = co2Levels[9];
            }
            else if (time > 08 && time < 16)
            {
                result = co2Levels[10];
            }
            else
            {
                result = co2Levels[11];
            }
        }
        if (currentDay == "Friday")
        {
            if (time < 08)
            {
                result = co2Levels[12];
            }
            else if (time > 08 && time < 16)
            {
                result = co2Levels[13];
            }
            else
            {
                result = co2Levels[14];
            }
        }
        if (currentDay == "Saturday")
        {
            if (time < 08)
            {
                result = co2Levels[15];
            }
            else if (time > 08 && time < 16)
            {
                result = co2Levels[16];
            }
            else
            {
                result = co2Levels[17];
            }
        }
        if (currentDay == "Sunday")
        {
            if (time < 08)
            {
                result = co2Levels[18];
            }
            else if (time > 08 && time < 16)
            {
                result = co2Levels[19];
            }
            else
            {
                result = co2Levels[20];
            }
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
