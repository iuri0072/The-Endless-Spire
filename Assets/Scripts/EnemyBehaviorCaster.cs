using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorCaster : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject spell;
    public Transform spellPos;
    public float attackDistance = 15;
    public enum State { statePatrol, stateAttack };
    public State state;
    private State originalState;
    private State lastState;

    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private GameObject player;
    private float timer;
    public float timeBtwShots;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        player = GameObject.FindWithTag("Player");

        originalState = state;
        lastState = originalState;

        pointA.transform.parent = null;
        pointB.transform.parent = null;
    }

    private void Update()
    {
        

        if (VerifyState() == State.statePatrol)
        {
            Patrol();
        }
        if (VerifyState() == State.stateAttack)
        {
            timer += Time.deltaTime;
            if (timer > timeBtwShots)
                Attack();

            Vector2 dir = (transform.position - player.transform.position).normalized;
            Vector3 localScale = transform.localScale;
            if (dir.x < 0)
                localScale.x = Mathf.Abs(localScale.x);
            else
                localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    private void Attack()
    {
        Instantiate(spell, spellPos.position, Quaternion.identity);
        timer = 0;
    }

    private void Patrol()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
            rb.velocity = new Vector2(speed, 0);
        else
            rb.velocity = new Vector2(-speed, 0);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }

    private State VerifyState()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
        {
            if (lastState == State.stateAttack)
            {
                ResetSpriteDirection();
                lastState = State.statePatrol;
            }
            anim.SetTrigger("Patrol");
            return State.statePatrol;
        }

        else
        {
            if (lastState == State.statePatrol)
            {
                lastState = State.stateAttack;
                timer = timeBtwShots;
            }
            anim.SetTrigger("Attack");
            return State.stateAttack;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void ResetSpriteDirection()
    {
        currentPoint = pointB.transform;

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
}
