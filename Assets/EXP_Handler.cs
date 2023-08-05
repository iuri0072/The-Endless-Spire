using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXP_Handler : MonoBehaviour
{
    [SerializeField]
    TMP_Text OrbCount;
    [SerializeField]
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
        if (OrbCount.text != ExperiencePoints.ToString())
        {
            OrbCount.text = ExperiencePoints.ToString();
        }
    }
    public void OnOrbPickup()
    {
        //Debug.Log("Made it to EXP_Handler_Class. Count is "+ExperiencePoints);
        ExperiencePoints = ExperiencePoints + 1;
    }
    public void OnOrbUse(int cost)
    {
        ExperiencePoints = ExperiencePoints - cost;
    }
}
