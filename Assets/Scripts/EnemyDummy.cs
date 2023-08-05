using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private GameObject player;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        FacePlayerDirection();
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
}
