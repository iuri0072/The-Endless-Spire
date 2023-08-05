using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagement : MonoBehaviour
{
    public Image[] hearths;

    public Sprite fullHearth;
    public Sprite emptyHearth;

    public TextMeshProUGUI atkDmg;
    public TextMeshProUGUI atkSpeed;
    public TextMeshProUGUI critPerc;
    public TextMeshProUGUI critMult;


    private GameObject player;
    private PlayerAttributes playerAttributes;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //UpdateWeaponStats();
    }

    private void Update()
    {
        UpdateHearthsArray();
        UpdateWeaponStats();
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

    private void UpdateWeaponStats()
    {
        atkDmg.text = "Dmg: " + player.GetComponent<PlayerCombat>().attackDamage;
        atkSpeed.text = "Speed: " + player.GetComponent<PlayerCombat>().attackRate;
        critPerc.text = "Crit: " + player.GetComponent<PlayerCombat>().critChance + "%";
        critMult.text = "Crit Dmg: " + player.GetComponent<PlayerCombat>().critMultiplier + "x";
    }
}
