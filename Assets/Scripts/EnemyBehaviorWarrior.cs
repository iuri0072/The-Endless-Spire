using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorWarrior : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float chaseDistance = 10;
    public enum State { statePatrol, stateChase, stateWait };
    public State state;
    private State originalState;
    private State lastState;

    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private GameObject player;
    private GameObject bodyCenter;
    private bool animInProgress;

    private void Awake()
    {
        pointA = new GameObject("PointA");
        pointB = new GameObject("PointB");
        ResetPatrolPointPositions();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        player = GameObject.FindWithTag("Player");
        animInProgress = false;

        originalState = state;
        lastState = originalState;

        bodyCenter = new GameObject("Body Center");
        bodyCenter.transform.parent = transform;
        bodyCenter.transform.localPosition = new Vector3(0, 1.5f, 0);

        pointA.transform.parent = null;
        pointB.transform.parent = null;
    }

    private void Update()
    {
        if (VerifyState() == State.statePatrol)
        {
            Patrol();
        }
        if(VerifyState() == State.stateChase)
        {
            Chase();
        }
        if (Mathf.Round(pointA.transform.position.y) != Mathf.Round(transform.position.y + 1.5f))
        {
            //Debug.Log("Point A's Y Pos: " + pointA.transform.position.y + ", Enemy Y Pos: " + this.transform.position.y);
            pointA.transform.position = new Vector2(transform.position.x - 4.5f, transform.position.y + 1.5f);
        }
        if (Mathf.Round(pointB.transform.position.y) != Mathf.Round(transform.position.y + 1.5f))
        {
            pointB.transform.position = new Vector2(transform.position.x + 4.5f, transform.position.y + 1.5f);
        }
        if (pointB.transform.position.x < transform.position.x + .1f || pointA.transform.position.x > transform.position.x - .1f)
        {
            //Debug.Log("Enemy is out of patrol range");
            ResetPatrolPointPositions();
        }
    }

    private void Chase()
    {
        var step = speed * Time.deltaTime;
        Vector2 chaseDestination = new Vector2 (player.transform.position.x , transform.position.y); 
        transform.position =  Vector2.MoveTowards(transform.position, chaseDestination, step);

        Vector2 dir = (transform.position - player.transform.position).normalized;
        Vector3 localScale = transform.localScale;
        if (dir.x < 0)
            localScale.x = Mathf.Abs(localScale.x);
        else
            localScale.x = -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    private void Patrol()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
            rb.velocity = new Vector2(speed, 0);
        else
            rb.velocity = new Vector2(-speed, 0);

        if (Vector2.Distance(bodyCenter.transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(bodyCenter.transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }

    private State VerifyState()
    {
        if (animInProgress)
            return State.stateWait;

        if (Vector2.Distance(bodyCenter.transform.position, player.transform.position) > chaseDistance)
        {
            if(lastState == State.stateChase)
            {
                ResetPatrolPointPositions();
                ResetSpriteDirection();
                lastState = State.statePatrol;
            }
            anim.SetTrigger("Patrol");
            return State.statePatrol;
        }

        else
        {
            if (lastState == State.statePatrol)
                lastState = State.stateChase;
            anim.SetTrigger("Chase");
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
    private void ResetPatrolPointPositions()
    {
        pointA.transform.position = new Vector2(transform.position.x - 4.5f, transform.position.y + 1.5f);
        pointB.transform.position = new Vector2(transform.position.x + 4.5f, transform.position.y + 1.5f);
    }
    private void ResetSpriteDirection()
    {
        currentPoint = pointB.transform;

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
    private void AnimStart()
    {
        animInProgress = true;
    }
    private void AnimEnd()
    {
        animInProgress = false;
    }
}
