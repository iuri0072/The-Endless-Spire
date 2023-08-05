using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private int atkPower = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //code for damaging player

            if (player.GetComponent<PlayerAttributes>().ivunerable == false)
                DoDamage();
        }
    }

    void DoDamage()
    {
        player.GetComponent<PlayerAttributes>().TakeDamage(atkPower);
        player.GetComponent<PlayerAttributes>().ivunerable = true;
    }
}
