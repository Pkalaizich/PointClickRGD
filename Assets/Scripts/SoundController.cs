using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private List<AudioClip> aliens;

    [SerializeField] private AudioClip discoverClip;

    [SerializeField] private AudioClip winLoop;

    [SerializeField] private float timeToPlayAliens;

    [SerializeField] private AudioClip sliderPiece;

    [SerializeField] private AudioClip selection;

    private float lastTimeAliensSound;

    private void Start()
    {
        lastTimeAliensSound = Time.time;
        MakeSoundFade(1);
    }

    private void Update()
    {
        if(Time.time - lastTimeAliensSound > timeToPlayAliens)
        {
            PlayAlienSound();
        }
        //if(Input.GetKeyDown(KeyCode.Escape)) { PlayDiscover(); }
    }

    public void PlayAlienSound()
    {
        int i = Random.Range(0, aliens.Count);
        oneShotSource.PlayOneShot(aliens[i]);
        lastTimeAliensSound= Time.time;
    }

    public void PlayDiscover()
    {
        //oneShotSource.time = 3.25f;
        oneShotSource.PlayOneShot(discoverClip);        
    }

    public void PlaySlider()
    {
        oneShotSource.PlayOneShot(sliderPiece);
    }

    public void ChangeMusicToWin()
    {
        StartCoroutine(WinChange());
    }

    private IEnumerator WinChange()
    {
        MakeSoundFade(-1);
        yield return new WaitForSeconds(1f);
        backgroundMusic.clip= winLoop;
        backgroundMusic.Play();
        MakeSoundFade(1);
    }

    public void MakeSoundFade(float duration)
    {
        StartCoroutine(FadeSound(duration));
    }

    public void PlaySelection()
    {
        oneShotSource.PlayOneShot(selection);
    }

    private IEnumerator FadeSound(float duration)
    {
        float multiplier = 1f;
        if(duration<0)
        {
            multiplier = -1f;
        }
        for(int i=0; i<20;i++)
        {
            backgroundMusic.volume += 1.0f / 20f * multiplier;
            yield return new WaitForSeconds(duration / 20 * multiplier);
        }
        //yield return new WaitForSeconds(0);
    }

}
