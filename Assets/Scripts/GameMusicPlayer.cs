using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
    [SerializeField]private AudioSource[] _audioSource;
    public int TrackPlaying;
    public bool TrackIsPlaying = false;
    public float transitionTime = 1.25f;
    public float defaultVolume = 0.4f;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        PlayMusic(0);
    }

    public void PlayMusic(int index)
    {
        if (TrackIsPlaying) return;
        _audioSource[index].Play();
    }

    public void StopMusic(int index)
    {
        _audioSource[index].Stop();
    }
    public void CheckTrackPlaying()
    {
        //TrackPlaying = -1;
        for(int i = 0; i < _audioSource.Length; i++)
        {
            if (_audioSource[i].isPlaying)
            {
                TrackPlaying = i;
            }
        }
    }
    public int GetTrackPlaying()
    {
        return TrackPlaying;
    }
    public void ChangeClip(int from, int to)
    {
        //StartCoroutine(Mix)
        AudioSource nowPlaying = _audioSource[from];
        AudioSource target = _audioSource[to];
        if (nowPlaying.isPlaying)
        {
            nowPlaying = _audioSource[to];
            target = _audioSource[from];
        }
        StartCoroutine(MixSources(nowPlaying, target));
    }
    IEnumerator MixSources(AudioSource nowPlaying, AudioSource target)
    {
        float percentage = 0;
        while (nowPlaying.volume > 0)
        {
            nowPlaying.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
        nowPlaying.Pause();
        if(target.isPlaying == false)
        {
            target.Play();
        }target.UnPause();
        percentage = 0;
        while (target.volume < defaultVolume)
        {
            nowPlaying.volume = Mathf.Lerp( 0, defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
    }

}


