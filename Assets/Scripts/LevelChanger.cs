using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public bool nextLevel;
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    FadeToNextLevel();
    }

    public void FadeToLevel(int levelIndex)
    {
        
        if (nextLevel)
            levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        else
            levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void NextLevelNoFade(int levelIndex)
    {
        if(nextLevel)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(levelToLoad);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);

    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadThisLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
