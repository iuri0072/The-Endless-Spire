using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip click;

    public void Click()
    {
        src.clip = click;
        src.Play();
    }
}
