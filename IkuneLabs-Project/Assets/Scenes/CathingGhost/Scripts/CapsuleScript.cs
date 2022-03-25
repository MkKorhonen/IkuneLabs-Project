using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    StunnedGhost stunnedGhost;

    private void OnTriggerEnter(Collider other)
    {
        //When the capsule touches stunned ghost prefab it destroys the capsule and calls GhostCaptured function

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextRay>();
        Destroy(gameObject);
        stunnedGhost = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StunnedGhost>();
        stunnedGhost.GhostCaptured();
    }
}
