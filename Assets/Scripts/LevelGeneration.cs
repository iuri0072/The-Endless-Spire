using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT
    public GameObject player;
    public GameObject mainCamera;

    private int direction;
    public float moveAmountX;
    public float moveAmountY;

    public float minX;
    public float maxX;
    public float maxY;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public bool stopGeneration;
    public LayerMask room;
    private int upCounter;

    

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        //Instantiate(player, transform.position, Quaternion.identity);
        player.transform.position = transform.position;

        mainCamera.GetComponent<CameraControl>().updateLookAt(player.transform);

        stopGeneration = false;
        direction = Random.Range(1, 6);
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
                upCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

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
                upCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
                direction = 5;
        }
        else if (direction == 5) //move up
        {
            upCounter++;
            if (transform.position.y < maxY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                //print(roomDetection);
                if (roomDetection.GetComponent<RoomType>().type != 2 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (upCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randTopRoom = Random.Range(2, 4);
                        //if (randTopRoom == 1)
                        //    randTopRoom = 2;
                        Instantiate(rooms[randTopRoom], transform.position, Quaternion.identity);
                    }

                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmountY);
                transform.position = newPos;

                int rand = Random.Range(1, 4);
                if (rand == 2)
                    rand = 1;
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else //stop generation
            {
                stopGeneration = true;
            }
        }

    }
}
