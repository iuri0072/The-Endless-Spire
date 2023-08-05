using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    private Animator anim;
    public bool dummy;

    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, bool critDmg)
    {
        //print("damage taken: " + damage);
        Vector3 posRandomness = new Vector3(Random.Range(0f, 0.25f), Random.Range(2f, 2.25f), 0f);
        if(critDmg)
            DamagePopUpGenerator.current.CreatePopUp(transform.position + posRandomness, damage.ToString(), Color.blue);
        else
            DamagePopUpGenerator.current.CreatePopUp(transform.position + posRandomness, damage.ToString(), Color.yellow);
        if (dummy)
            return;
        currentHealth -= damage;

        //Play hurt animation
        anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Die animation
        anim.SetBool("isDead", true);
        //this.GetComponent<OnDeath>().enabled = true;
        //Disable the enemy
        print("enemy died");
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
