using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        MakeSoundFade(1);
    }

    public void MakeSoundFade(float duration)
    {
        StartCoroutine(FadeSound(duration));
    }



    private IEnumerator FadeSound(float duration)
    {
        float multiplier = 1f;
        if (duration < 0)
        {
            multiplier = -1f;
        }
        for (int i = 0; i < 20; i++)
        {
            backgroundMusic.volume += 1.0f / 20f * multiplier;
            yield return new WaitForSeconds(duration / 20f * multiplier);
        }
        //yield return new WaitForSeconds(0);
    }
}
