using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class ExitDoors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private List<EventScriptable> requiredEvents;
    [SerializeField] private Outline otherDoorOutline;
    [SerializeField] private string lockedMessage;
    [SerializeField] private GameObject winPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GamePlayEvents.Instance.RequiredEvents(requiredEvents))
        {
            FindObjectOfType<SoundController>().ChangeMusicToWin();
            winPanel.SetActive(true);
        }
        else
        {
            UIController.Instance.SetDialog(lockedMessage);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entrando");
        GetComponent<Outline>().eraseRenderer = false;
        otherDoorOutline.eraseRenderer = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Outline>().eraseRenderer = true;
        otherDoorOutline.eraseRenderer = true;
    }



}
