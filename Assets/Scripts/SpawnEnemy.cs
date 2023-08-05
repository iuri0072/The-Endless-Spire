using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objects;
    public float distanceFromPlayer;
    public GameObject player;
    public float SpawnRange = 20;
    public bool SpawnConsumed = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if(distanceFromPlayer <= SpawnRange && !SpawnConsumed)
        {
            spawnEnemy();
            SpawnConsumed = true;
        }
    }
    private void spawnEnemy()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
