using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    public int damage = 1;

    public float moveSpeed;
    public float attackRange;
    private Camera cam;
    private Vector3 player;
    TextRay textRay;

    private void Start()
    {
        cam = Camera.main;
        textRay = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextRay>();
    }

    private void Update()
    {
        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        player = cam.transform.position;
        player.y = -1;

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
}
