using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public bool nextLevel;
    public float distanceFromPlayer;
    public GameObject player;
    public float NextLevelRange = 5;
    public GameObject Exit;
    public GameMusicPlayer gmp;
    public bool bossDefeated = false;
    public bool exitFound = false;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Exit = GameObject.FindGameObjectWithTag("Exit");
        gmp = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<GameMusicPlayer>();
    }
    void Update()
    {
        //Checks distance from player before proceeding to next level.
        if (Exit == null)
        {
            Exit = GameObject.FindGameObjectWithTag("Exit");
        }
        else if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            distanceFromPlayer = Vector3.Distance(player.transform.position, Exit.transform.position);
        }
        //if (Input.GetMouseButtonDown(0))
        //    FadeToNextLevel();
        if (distanceFromPlayer <= NextLevelRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("exit key pressed");
                if (Exit.gameObject.tag == "Exit")
                {
                    //Debug.Log("Game object tag equals Exit");
                    if (SceneManager.GetActiveScene().name == "TutorialScene")
                    {
                        gmp.CheckTrackPlaying();
                        int trackIndex = gmp.GetTrackPlaying();
                        //print(trackIndex);
                        //Debug.Log("Attempting to change clip from"+trackIndex+" to "+(trackIndex+1));
                        gmp.FadingMusic(trackIndex, (trackIndex + 1));
                        int index = SceneManager.GetActiveScene().buildIndex + 1;
                        FadeToLevel(index);
                    }else if(SceneManager.GetActiveScene().name == "Lvl-1")
                    {
                        gmp.CheckTrackPlaying();
                        int trackIndex = gmp.GetTrackPlaying();
                        //print(trackIndex);
                        //Debug.Log("Attempting to change clip from"+trackIndex+" to "+(trackIndex+1));
                        gmp.FadingMusic(2, 1);
                        ReloadThisLevel();
                        //Debug.Log("Attempting to Exit");
                    }
                }
            }
        }
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

    private void resetBools()
    {
        bossDefeated = false;
        exitFound = false;
    }
}
