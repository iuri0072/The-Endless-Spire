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
    public enum State { statePatrol, stateAttack, stateWait };
    public State state;
    private State originalState;
    private State lastState;

    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private GameObject player;
    private GameObject bodyCenter;
    //private float timer;
    private bool animInProgress;
    public float timeBtwShots;
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
        if (VerifyState() == State.stateAttack)
        {
            /*timer += Time.deltaTime;
            if (timer > timeBtwShots)
                Attack();*/
            FacePlayerDirection();
            
        }
        if(Mathf.Round(pointA.transform.position.y) != Mathf.Round(this.transform.position.y + 1.5f))
        {
            //Debug.Log("Point A's Y Pos: " + pointA.transform.position.y + ", Enemy Y Pos: " + this.transform.position.y);
            pointA.transform.position = new Vector2(transform.position.x - 4.5f, transform.position.y + 1.5f);
        }
        if (Mathf.Round(pointB.transform.position.y) != Mathf.Round(this.transform.position.y + 1.5f))
        {
            pointB.transform.position = new Vector2(transform.position.x + 4.5f, transform.position.y + 1.5f);
        }
        if (pointB.transform.position.x < transform.position.x || pointA.transform.position.x > transform.position.x)
        {
            //Debug.Log("Enemy is out of patrol range");
            ResetPatrolPointPositions();
        }
    }

    private void Attack()
    {
        Instantiate(spell, spellPos.position, Quaternion.identity);
        //timer = 0;
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

        if (Vector2.Distance(bodyCenter.transform.position, player.transform.position) > attackDistance)
        {
            if (lastState == State.stateAttack)
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
                lastState = State.stateAttack;
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
    private void ResetPatrolPointPositions()
    {
        pointA.transform.position = new Vector2(transform.position.x - 4.5f, transform.position.y + 1.5f);
        pointB.transform.position = new Vector2(transform.position.x + 4.5f, transform.position.y + 1.5f);
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
    private void FacePlayerDirection()
    {
        Vector2 dir = (transform.position - player.transform.position).normalized;
        Vector3 localScale = transform.localScale;
        if (dir.x < 0)
            localScale.x = Mathf.Abs(localScale.x);
        else
            localScale.x = -Mathf.Abs(localScale.x);
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
