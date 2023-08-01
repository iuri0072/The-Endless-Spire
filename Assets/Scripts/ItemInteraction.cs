using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public GameObject[] objects;
    public float distanceFromPlayer;
    public GameObject player;
    public float DisplayTextRange = 5;
    public GameObject itemDescription;
    [SerializeField] private Transform[] points;
    [SerializeField] private ItemLineController item_LR;
    [SerializeField] private Transform uiPointA;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (distanceFromPlayer <= DisplayTextRange)
        {
            //Debug.Log("Player Near Item");
            DisplayInformation();
        }
        if (distanceFromPlayer >= DisplayTextRange && itemDescription.activeSelf)
        {
            HideDisplayInfo();
            Debug.Log("Player moved out of items range");
        }
    }
    private void DisplayInformation()
    {
        //itemDescription = GameObject.FindGameObjectWithTag("DisplayPane");
        itemDescription.SetActive(true);
        Transform[] _points = new Transform[] { this.transform, uiPointA };
        points = _points;
        item_LR.SetUpLine(points);
        //Debug.Log("Player Near Item");
    }
    private void HideDisplayInfo()
    {
        itemDescription.SetActive(false);
        item_LR.DestroyLine();
    }
}
