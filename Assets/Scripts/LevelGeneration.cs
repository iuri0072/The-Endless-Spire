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

    public float minX;
    public float maxX;
    public float maxY;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    private bool stopGeneration;

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        stopGeneration = false;
        direction = Random.Range(1, 6);
        print(direction);
    }

    private void Update()
    {
        if(timeBtwRoom <= 0 && stopGeneration == false)
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
            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                transform.position = newPos;

                direction = Random.Range(1, 6);
                if (direction == 3)
                    direction = 1;
                else if (direction == 4)
                    direction = 5;
            }
            else
                direction = 5;
        }
        else if (direction == 3 || direction == 4) //move left
        {
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
                transform.position = newPos;

                direction = Random.Range(3, 6);
            }
            else
                direction = 5;
        }
        else if (direction == 5) //move up
        {
            if (transform.position.y < maxY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmountY);
                transform.position = newPos;

                direction = Random.Range(1, 6);
            }
            else //stop generation
            {
                stopGeneration = true;
            }
        }

        print(direction);
        Instantiate(rooms[0], transform.position, Quaternion.identity);
    }
}
