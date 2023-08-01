using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    Animator anim;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }
    public void CreditsTransitionIn()
    {
        if (!anim.GetBool("isViewingCredits"))
        {
            anim.SetBool("isLeavingCredits", false);
            anim.SetBool("isViewingCredits", true);
        }
    }
    public void CreditsTransitionOut()
    {
        if (anim.GetBool("isViewingCredits") && !anim.GetBool("isLeavingCredits"))
        {
            anim.SetBool("isViewingCredits", false);
            anim.SetBool("isLeavingCredits", true);
        }
    }
}
