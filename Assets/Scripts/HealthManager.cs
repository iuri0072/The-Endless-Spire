using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 10;
    public Image[] hearths;

    public Sprite fullHearth;
    public Sprite emptyHearth;

    private void Update()
    {
        foreach (Image img in hearths)
        {
            img.sprite = emptyHearth;
        }
        for (int i = 0; i < health; i++)
        {
            hearths[i].sprite = fullHearth;
        }

    }

}
