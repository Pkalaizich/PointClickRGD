using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderPedestal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private BoxCollider objectCollider;
    private Outline outline;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<Player>().GoToSlider();
        FindObjectOfType<SliderParent>().SetPiecesInteractable(true);
        FindObjectOfType<SliderParent>().SetButton();
        outline.eraseRenderer = true;
        UIController.Instance.ControlRotationButtons(false);
        UIController.Instance.ShowBackBtn();
        UIController.Instance.SetInspectorVisibility(true);
        this.enabled = false;
        objectCollider.enabled = false;
    }
    private void Awake()
    {
        outline = GetComponent<Outline>();
        objectCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        FindObjectOfType<SliderParent>().SetPiecesInteractable(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.eraseRenderer = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.eraseRenderer = true;
    }

    public void EnableComponents(bool enable)
    {
        objectCollider.enabled = enable;
    }
}
