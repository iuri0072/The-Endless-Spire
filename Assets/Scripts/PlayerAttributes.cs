using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    private Animator anim;

    public int maxHealth = 1000;
    public int currentHealth;
    public bool ivunerable = false;

    public float ivunerabilityTime = 0;
    public float resetTimer = 2f;



    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if(ivunerable == true)
        {
            anim.SetLayerWeight(1, 1);
            ivunerabilityTime += Time.deltaTime;
            if(ivunerabilityTime >= resetTimer)
            {
                ivunerable = false;
                ivunerabilityTime = 0;
                anim.SetLayerWeight(1, 0);
            }
        }
    }

    void Die()
    {
        //Die animation
        anim.SetBool("isDead", true);
        //Disable the enemy
        print("enemy died");
    }

}
