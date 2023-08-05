using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public LayerMask enemyLayers;

    public int attackDamage = 15;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public int critChance = 10;
    public float critMultiplier = 2;

    public float attackRate = 1f;
    float nextAttackTime = 0f;
    
    private void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        
    }

    void Attack()
    {
        float currentDmg = attackDamage;

        var willCrit = Random.Range(0, 101);
        if(willCrit <= critChance)
            currentDmg *= critMultiplier;

        //Play an attack animation
        anim.SetTrigger("Attack");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAttributes>().TakeDamage((int)currentDmg);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        i'll change the dmging syst in here, hold on dudes!
    }*/

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
