using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeOutScreen : MonoBehaviour
{
    Image darkScreen;

    private void Awake()
    {
        darkScreen= GetComponent<Image>();
    }

    private void OnEnable()
    {
        darkScreen.DOFade(0f, 1f);
    }

    public void FadeOut()
    {
        darkScreen.DOFade(1f, 1f);
    }
}
