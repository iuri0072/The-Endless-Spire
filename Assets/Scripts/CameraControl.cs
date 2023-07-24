using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject gameManager;
    public GameObject backgroundGroup;
    public Transform player;
    private Transform lookAt;
    public bool mapStatic = true;
    public float boundX = .15f;
    public float boundY = .05f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void LateUpdate()
    {
        if(lookAt.tag == "Player" && !mapStatic)
        {
            Vector3 delta = Vector3.zero;
            
            float deltaX = lookAt.position.x - transform.position.x;
            float deltaY = lookAt.position.y - transform.position.y;

            //Check if the player is inside the bounds for X
            if (deltaX > boundX || deltaX < -boundX)
            {
                if (transform.position.x < lookAt.position.x)
                {
                    delta.x = deltaX - boundX;
                }
                else
                {
                    delta.x = deltaX + boundX;
                }
            }

            //Check if the player is inside the bounds for Y
            if (deltaY > boundY || deltaY < -boundY)
            {
                if (transform.position.y < lookAt.position.y)
                {
                    delta.y = deltaY - boundY;
                }
                else
                {
                    delta.y = deltaY + boundY;
                }
            }

            transform.position += new Vector3(delta.x, delta.y, 0);
        }
        else if (mapStatic)
        {
            int coordX;
            int coordY;

            coordX = (int)((player.position.x + 27) / 54);
            coordY = (int)((player.position.y + 15) / 30);

            transform.localPosition = new Vector3(coordX * 54, coordY * 30, -10);
        }
    }

    public void updateLookAt(Transform newTarget)
    {
        lookAt = newTarget;
    }
}
