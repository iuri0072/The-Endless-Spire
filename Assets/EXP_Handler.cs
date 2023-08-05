using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXP_Handler : MonoBehaviour
{
    TMP_Text OrbCount;
    int ExperiencePoints;
    // Start is called before the first frame update
    void Start()
    {
        OrbCount = GameObject.FindGameObjectWithTag("Experience").GetComponent<TMP_Text>();
        ExperiencePoints = 0;
        OrbCount.text = ExperiencePoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(ExperiencePoints.ToString() != OrbCount.text)
        {
            OrbCount.text = ExperiencePoints.ToString();
        }    
    }
    public void OnOrbPickup()
    {
        ExperiencePoints += 1;
    }
    public void OnOrbUse(int cost)
    {
        ExperiencePoints = ExperiencePoints - cost;
    }
}
