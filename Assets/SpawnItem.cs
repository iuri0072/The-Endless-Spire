using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objects;
    public GameObject player;
    public bool SpawnConsumed = false;
    public bool AddingForce = false;
    public int forceMin = -10;
    public int forceMax = 10;
    [SerializeField] private int itemCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpawnConsumed)
        {
            SpawnAnItem(itemCount);
        }
    }
    public void SetItemCount(int c)
    {
        itemCount = c;
    }
    private void SpawnAnItem(int count)
    {
        for (int i = 0; i < count; i++) {
            int rand = Random.Range(0, objects.Length);
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            if (AddingForce) {
                float force = (float)Random.Range(forceMin, forceMax);
                Vector2 direction = new Vector2((Random.Range(forceMin, forceMax)), (Random.Range(forceMin, forceMax)));
                instance.GetComponent<Rigidbody2D>().AddForce(direction*force*force);
                
            }
        }
        SpawnConsumed = true;
    }
}
