using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public GameObject[] objects;
    public float distanceFromPlayer;
    public GameObject player;
    public PlayerMovement pm;
    public float DisplayTextRange = 5;
    public GameObject itemDescription;
    [SerializeField] private Transform[] points;
    [SerializeField] private ItemLineController item_LR;
    [SerializeField] private Transform uiPointA;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        
        if (distanceFromPlayer <= DisplayTextRange)
        {
            if (!pm.itemCurrentlySelected)
            {
                //Debug.Log("Player Near Item");
                pm.SetSelectedItem(this.gameObject);
                DisplayInformation();
            }
            else
            {
                //Debug.Log("Item already Selected");
            }
            
        }
        if (distanceFromPlayer >= DisplayTextRange && itemDescription.activeSelf)
        {
            HideDisplayInfo();
            pm.DeSelectItem();
            //Debug.Log("Player moved out of items range");
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
