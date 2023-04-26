using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject asociatedPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("CLickeado");
        FindObjectOfType<UIController>().Open3Dviewer();
        FindObjectOfType<Item3DViewer>().SpawnObject(asociatedPrefab);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entrando");
        GetComponent<Outline>().eraseRenderer = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Outline>().eraseRenderer = true;
    }


    
}
