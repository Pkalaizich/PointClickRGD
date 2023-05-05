using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(int i)
    {
        StartCoroutine(FadeSoundAndImage(i));
    }

    private IEnumerator FadeSoundAndImage(int i)
    {
        FindObjectOfType<FadeOutScreen>().FadeOut();
        if(SceneManager.GetActiveScene().buildIndex ==0)
        {
            FindObjectOfType<MenuSoundController>().MakeSoundFade(-1f);
        }
        else
        {
            FindObjectOfType<SoundController>().MakeSoundFade(-1f);
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(i);
    }
}
