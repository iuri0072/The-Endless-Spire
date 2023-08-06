using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fieldCheckers;
    public GameObject player;
    [SerializeField] private float[] fieldCheckersDistance;
    public float ActivationCheckRange;
    GameMusicPlayer gmp;

    private void Start()
    {
        gmp = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<GameMusicPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(gmp == null)
        {
            gmp = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<GameMusicPlayer>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        for (int i = 0; i < fieldCheckers.Length; i++)
        {
            fieldCheckersDistance[i] = Vector3.Distance(player.transform.position, fieldCheckers[i].transform.position);
             
            //checker.transform.GetChild(0).gameObject.GetComponent<Text>().text = _dist.ToString();
        }
        //Performs check that player is within room without use of colliders to avoid grappling hook collisions
        if((fieldCheckersDistance[0] < 53 && fieldCheckersDistance[1] < 53)&&(fieldCheckersDistance[2] < 30 && fieldCheckersDistance[3] < 30))
        {
            //Debug.Log("Player is is room.");
            gmp.CheckTrackPlaying();
            int trackIndex = gmp.GetTrackPlaying();
            //print(trackIndex);
            Debug.Log("Entered Boss room, change clip from"+trackIndex+" to "+(trackIndex+1));
            gmp.FadingMusic(trackIndex, (trackIndex + 1));
        }
    }
}
