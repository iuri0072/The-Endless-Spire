using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject objects;
    // Start is called before the first frame update
    private void Start()
    {
        this.enabled = false;
    }
    public void SpawnItems()
    {
        int count = 2;
        for (int i = 0; i < count; i++)
        {
            objects.GetComponent<SpawnItem>().SetItemCount(1);
            objects.GetComponent<SpawnItem>().enabled = true;
            GameObject gm = GameObject.Find("GameManager");
            GameObject instance = (GameObject)Instantiate(objects, transform.position, Quaternion.identity);
            //GameObject instance = (player.gameObject)Instantiate(objects, transform.position, Quaternion.identity)
            instance.transform.parent = gm.transform;
        }
    }
}
