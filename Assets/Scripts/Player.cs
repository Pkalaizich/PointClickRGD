using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_RotationStepAngle;
    Sequence moveSequence;


    public void Rotation(bool right)
    {
        float rotation = right ? m_RotationStepAngle : -m_RotationStepAngle;
        float finalAngle = Mathf.Round((this.transform.localRotation.y + rotation) / 45)*45;
        this.transform.DORotate(new Vector3(0,finalAngle,0), 1,RotateMode.LocalAxisAdd);
    }

    public void GoToSlider()
    {
        moveSequence = DOTween.Sequence();
        moveSequence.Append(transform.DOMoveZ(-8f, 1f)).Append(transform.DORotate(new Vector3(20, this.transform.rotation.y, this.transform.rotation.z), 0.5f,RotateMode.LocalAxisAdd)).SetAutoKill(false);
        
    }

    public void BackToNormal()
    {
        moveSequence.SmoothRewind();
    }

    
}
