using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float chaseDistance = 10;
    public enum State { statePatrol, stateChase };
    public State state;
    private State originalState;
    private State lastState;

    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private GameObject player;


    /*private void OnEnable()
    {
        pointA.transform.position = new Vector2(transform.position.x - 4.5f, transform.position.y);
        pointB.transform.position = new Vector2(transform.position.x + 4.5f, transform.position.y);
    }*/

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetTrigger("Patrol");
        player = GameObject.FindWithTag("Player");

        originalState = state;
        lastState = originalState;

        pointA.transform.parent = null;
        pointB.transform.parent = null;
    }

    private void Update()
    {
        if(VerifyState() == State.statePatrol)
        {
            Patrol();
        }
        if(VerifyState() == State.stateChase)
        {
            Chase();
        }
    }

    private void Chase()
    {
        var step = speed * Time.deltaTime;
        Vector2 chaseDestination = new Vector2 (player.transform.position.x , transform.position.y); 
        transform.position =  Vector2.MoveTowards(transform.position, chaseDestination, step);
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
        if (Vector2.Distance(transform.position, player.transform.position) > chaseDistance)
        {
            if(lastState == State.stateChase)
            {
                pointA.transform.position = new Vector2(transform.position.x - 4.5f, transform.position.y);
                pointB.transform.position = new Vector2(transform.position.x + 4.5f, transform.position.y);
                lastState = State.statePatrol;
            }
            return State.statePatrol;
        }

        else
        {
            if (lastState == State.statePatrol)
                lastState = State.stateChase;
            return State.stateChase;
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
}
