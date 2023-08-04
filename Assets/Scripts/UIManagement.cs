using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public Image[] hearths;

    public Sprite fullHearth;
    public Sprite emptyHearth;

    private GameObject player;
    private PlayerAttributes playerAttributes;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        UpdateHearthsArray();

    }

    private void UpdateHearthsArray()
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < player.GetComponent<PlayerAttributes>().maxHealth)
            {
                hearths[i].sprite = emptyHearth;
                hearths[i].color = new(255, 255, 255, 1);
            }
            else
            {
                hearths[i].sprite = null;
                hearths[i].color = new(255, 255, 255, 0);
            }
        }

        for (int i = 0; i < player.GetComponent<PlayerAttributes>().currentHealth; i++)
        {
            hearths[i].sprite = fullHearth;
        }
    }
}
