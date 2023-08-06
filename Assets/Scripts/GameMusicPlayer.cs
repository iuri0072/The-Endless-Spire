using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
    [SerializeField]private AudioSource[] _audioSource;
    public int TrackPlaying;
    public bool TrackIsPlaying = false;
    public float transitionTime = .25f;
    public float defaultVolume = .4f;
    public bool MusicIsFading = false;
    public bool FadeMusic = false;
    private float elapsedTime;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        PlayMusic(0);
    }
    public void Update()
    {

    }
    public void PlayMusic(int index)
    {
        if (TrackIsPlaying) return;
        _audioSource[index].volume = defaultVolume;
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
        
        //Debug.Log("Made it into change clip function");
        AudioSource nowPlaying = _audioSource[from];
        AudioSource target = _audioSource[to];
        if (nowPlaying.isPlaying)
        {
            nowPlaying = _audioSource[to];
            target = _audioSource[from];
        }
        //StartCoroutine(MixSources(nowPlaying, target));
    }
    //IEnumerator MixSources(AudioSource nowPlaying, AudioSource target)
    //{
    //    //Debug.Log("Made it into Mix Source coroutine");
    //    Debug.Log(nowPlaying.name + nowPlaying.isPlaying);
    //    float percentage = 0;
    //    while (nowPlaying.volume > 0)
    //    {
    //        nowPlaying.volume = Mathf.Lerp(defaultVolume, 0, percentage);
    //        percentage += Time.deltaTime / transitionTime;
    //        Debug.Log(nowPlaying.name + nowPlaying.isPlaying + nowPlaying.volume);
    //        yield return null;
    //    }

    //    nowPlaying.Stop();
    //    if (target.isPlaying == false)
    //    {
    //        target.Play();
    //    }
    //    target.UnPause();
    //    percentage = 0;
    //    while (target.volume < defaultVolume)
    //    {
    //        nowPlaying.volume = Mathf.Lerp(0, defaultVolume, percentage);
    //        Debug.Log(target.name + target.isPlaying + target.volume);
    //        percentage += Time.deltaTime / transitionTime;
    //        yield return null;
    //    }
    //}
    public void FadingMusic(int from, int to)
    {
        //FadeOut(_audioSource[0]);
        //StartCoroutine(FadeOut(_audioSource[0]));
        //StartCoroutine(FadeIn(_audioSource[1]));
        StartCoroutine(Fader(_audioSource[from], _audioSource[to]));
        //_audioSource[0].Stop();
        //_audioSource[1].Play();
    }

    private IEnumerator FadeOut(AudioSource asToFadeOut) 
    {
        float percentage = 0;
        float elapsedTime = 0;
        while (asToFadeOut.volume > 0)
        {

            asToFadeOut.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            elapsedTime += Time.fixedDeltaTime;
            percentage += elapsedTime / transitionTime;
            //Debug.Log("Time: " + elapsedTime + "Percent: " + (elapsedTime / transitionTime) + ", Volume: " + _audioSource[0].volume);

        }
        percentage = 0;
        elapsedTime = 0;
        yield return null;
    }
    private IEnumerator FadeIn(AudioSource asToFadeIn)
    {
        asToFadeIn.volume = 0;
        asToFadeIn.Play();
        float percentage = 0;
        float elapsedTime = 0;
        while (asToFadeIn.volume < defaultVolume)
        {
            elapsedTime += Time.deltaTime;
            percentage += elapsedTime / transitionTime;
            asToFadeIn.volume = Mathf.Lerp(0, defaultVolume,percentage);
            Debug.Log("Time: " + elapsedTime + "Percent: " + (elapsedTime / transitionTime) + ", Volume: " + _audioSource[0].volume);

        }
        percentage = 0;
        elapsedTime = 0;
        yield return null;
    }
    private IEnumerator Fader(AudioSource asToFadeOut,AudioSource asToFadeIn)
    {
        asToFadeIn.volume = 0;
        asToFadeIn.Play();
        float percentage = 0;
        float elapsedTime = 0;
        while (asToFadeIn.volume < defaultVolume && asToFadeOut.volume > 0)
        {
            asToFadeIn.volume = Mathf.Lerp(0, defaultVolume, percentage);
            asToFadeOut.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            elapsedTime += Time.fixedDeltaTime;
            percentage += elapsedTime / transitionTime;
            //Debug.Log("Time: " + elapsedTime + "Percent: " + (elapsedTime / transitionTime) + ", Volume: " + _audioSource[0].volume);

        }
        asToFadeOut.Stop();
        percentage = 0;
        elapsedTime = 0;
        yield return null;
    }
}


