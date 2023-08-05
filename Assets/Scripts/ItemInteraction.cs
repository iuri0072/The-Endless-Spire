using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private TMP_Text itemDetails;
    [SerializeField] private TMP_Text itemStats;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Image itemImage;
    public string textName;
    public string textStats;
    public string textDetails;
    public Sprite imagePortrait;
    private bool infoDisplayed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
        uiPointA = GameObject.Find("UIDisplayPointA").transform;
        itemImage = itemDescription.transform.GetChild(3).gameObject.GetComponent<Image>();
        itemDetails.text = textDetails;
        itemStats.text = textStats;
        itemName.text = textName;
        itemImage.sprite = imagePortrait;
    }


    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (this.GetComponent<ReverseGrappling>().isLerping && !pm.itemCurrentlySelected)
        {
            pm.SetSelectedItem(this.gameObject);
        }
        if (distanceFromPlayer <= DisplayTextRange && !this.GetComponent<ReverseGrappling>().isLerping)
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
        infoDisplayed = true;
    }
    private void HideDisplayInfo()
    {
        itemDescription.SetActive(false);
        item_LR.DestroyLine();
        infoDisplayed = false;
    }
    public void ActivateItem()
    {
        //Debug.Log("Player Activated Item");
        //Case Code goes here based on item type and data.
        string tag = this.gameObject.tag;
        if (infoDisplayed)
        {
            HideDisplayInfo();
        }
        switch (tag)
        {
            case "Experience":
                //Increase experience count via "OnOrbPickup()"
                GainedExperience();
                break;
            case "Potion":
                //Increase health via a function tbd.
                ConsumePotion();
                break;
        }

        Destroy(this.gameObject);
        pm.DeSelectItem();
    }
    private void GainedExperience()
    {
        GameObject.Find("AncestralOrbStat").GetComponent<EXP_Handler>().OnOrbPickup();
    }
    private void ConsumePotion()
    {
        if (player.GetComponent<PlayerAttributes>().currentHealth == player.GetComponent<PlayerAttributes>().maxHealth)
            return;
        player.GetComponent<PlayerAttributes>().currentHealth++;
    }
}
