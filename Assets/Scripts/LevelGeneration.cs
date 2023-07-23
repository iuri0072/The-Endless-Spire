using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    public float moveAmountX;
    public float moveAmountY;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if(timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if(direction == 1 || direction == 2) //move right
        {
            Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
            transform.position = newPos;
        }
        else if (direction == 3 || direction == 4) //move left
        {
            Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
            transform.position = newPos;
        }
        else if (direction == 5) //move up
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmountY);
            transform.position = newPos;
        }
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }
}
