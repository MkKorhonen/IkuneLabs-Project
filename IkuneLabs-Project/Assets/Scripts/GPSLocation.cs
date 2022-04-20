using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSLocation : MonoBehaviour
{
    private float longitudeBound1 = 25.472478f;
    private float longitudeBound2 = 25.459000f;
    private float latitudeBound1 = 65.062820f;
    private float latitudeBound2 = 65.055846f;
    private float playerCurLatitude, playerCurLongitude, playerLastLatitude, playerLastLongitude;
    private float centerLatitude = 65.059333f;
    private float centerLongitude = 25.465739f;
    public Text GPSStatus, latitudeValue, longitudeValue, boundsCheck;
    public CharacterController playerController;
    public GameObject center, left, right, top, bottom;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());
        playerController.transform.position = center.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GPSLoc()
    {
        yield return new WaitForSeconds(3);
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            GPSStatus.text = "GPS not enabled";
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait for service initialization
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't init in time
        if(maxWait < 1)
        {
            GPSStatus.text = "GPS timed out";
            yield break;
        }

        // Connection failed
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "GPS connection failed";
            yield break;
        }
        else
        {
            // Access granted
            GPSStatus.text = "Running GPS";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Access granted to GPS values and the service has been initialized
            GPSStatus.text = "Running GPS.";
            playerCurLatitude = Input.location.lastData.latitude;
            playerCurLongitude = Input.location.lastData.longitude;
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();

            if (playerCurLatitude < latitudeBound1 && playerCurLatitude > latitudeBound2 && playerCurLongitude < longitudeBound1 && playerCurLongitude > longitudeBound2)
            {
                boundsCheck.text = "Player within bounds";
                
                if(playerCurLatitude > centerLatitude && playerCurLatitude != playerLastLatitude)
                {
                    // Move up
                    //playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, top.transform.position, 5 * Time.deltaTime);
                }
                else
                {
                    // Move down
                    //playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, bottom.transform.position, 5 * Time.deltaTime);
                }

                if(playerCurLongitude > centerLongitude && playerCurLongitude != playerLastLongitude)
                {
                    // Move right
                    //playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, right.transform.position, 5 * Time.deltaTime);
                }
                else
                {
                    // Move left
                    //playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, left.transform.position, 5 * Time.deltaTime);
                }
            }
            else
            {
                boundsCheck.text = "Player not within bounds";
            }

            playerLastLatitude = playerCurLatitude;
            playerLastLongitude = playerCurLongitude;
        }
        else
        {
            // Service stopped
            GPSStatus.text = "GPS stopped";
        }
    }
}
