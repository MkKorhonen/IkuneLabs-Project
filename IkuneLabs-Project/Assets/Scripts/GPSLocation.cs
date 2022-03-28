using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSLocation : MonoBehaviour
{
    private float longitudeBound1 = 25.459000f;
    private float longitudeBound2 = 25.472478f;
    private float latitudeBound1 = 65.062820f;
    private float latitudeBound2 = 65.055846f;
    private float playerLatitude, playerLongitude;
    public Text GPSStatus, latitudeValue, longitudeValue, boundsCheck;
    public CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());
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
            GPSStatus.text = "GPS not enabled.";
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
            GPSStatus.text = "GPS time out.";
            yield break;
        }

        // Connection failed
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "GPS connection failed.";
            yield break;
        }
        else
        {
            // Access granted
            GPSStatus.text = "Running GPS.";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Access granted to GPS values and the service has been initialized
            GPSStatus.text = "Running GPS.";
            playerLatitude = Input.location.lastData.latitude;
            playerLongitude = Input.location.lastData.longitude;
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();

            if (playerLatitude < latitudeBound1 && playerLatitude > latitudeBound2 && playerLongitude < longitudeBound2 && playerLongitude > longitudeBound1)
            {
                boundsCheck.text = "Player within bounds";
                
                //Movement based on GPS coordinates, not working
                Vector3 playerCoordinates = Quaternion.AngleAxis(playerLongitude, -Vector3.up) * Quaternion.AngleAxis(playerLatitude, -Vector3.right) * new Vector3(0, 0, 1);
                playerController.Move(playerCoordinates * Time.deltaTime);
            }
            else
            {
                boundsCheck.text = "Player not within bounds";
            }
        }
        else
        {
            // Service stopped
            GPSStatus.text = "GPS stopped.";
        }
    }
}
