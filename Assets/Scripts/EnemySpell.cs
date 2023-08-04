using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    private int spellPower = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //code for damaging player

            if (player.GetComponent<PlayerAttributes>().ivunerable == false)
                DoDamage();
        }
    }

    void DoDamage()
    {
        player.GetComponent<PlayerAttributes>().TakeDamage(spellPower);
        player.GetComponent<PlayerAttributes>().ivunerable = true;
    }
}
