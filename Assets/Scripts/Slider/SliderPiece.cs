using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;
using Unity.VisualScripting;

public class SliderPiece : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public bool dragable = false;
    [SerializeField] private float dragSpeed =1;
    [SerializeField] private int currentSlot;
    static private SliderPiece currentPiece = null;

    private Rigidbody rb;
    private SliderParent sliderParent;

    private Vector3 offset;
    private Vector3 initialPosition;

    private Outline outline;

    private void OnEnable()
    {
        outline= GetComponent<Outline>();
        rb=GetComponent<Rigidbody>();
        sliderParent = GetComponentInParent<SliderParent>(); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(dragable)
        {
            float xMovement = Mathf.Clamp(eventData.delta.x * dragSpeed,-2f,2f);
            float zMovement = Mathf.Clamp(eventData.delta.y * dragSpeed, -2f, 2f);

            //this.transform.localPosition += new Vector3(xMovement,0,zMovement);

            //Debug.Log(xMovement);

            rb.velocity = xMovement * transform.right + zMovement * transform.forward + 0*transform.up;
            

            //Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            //transform.position = newPosition;
        }
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        rb.velocity = Vector3.zero;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentPiece = this;
        dragable = true;
        outline.eraseRenderer = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;/* | RigidbodyConstraints.FreezePositionX;    */    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentPiece ==null)
        {
            outline.eraseRenderer = false;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(currentPiece ==null)
        {
            outline.eraseRenderer = true;
        }        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragable= false;
        currentPiece = null;

        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.velocity= Vector3.zero;

        Transform initial = sliderParent.GetSlotPosition(currentSlot);
        Transform empty = sliderParent.GetSlotPosition(sliderParent.currentEmpty);

        float dist1 = Mathf.Abs(Vector3.Distance(this.transform.position,initial.position));
        float dist2 = Mathf.Abs(Vector3.Distance(this.transform.position, empty.position));

        if(dist2<dist1)
        {
            this.transform.position= empty.position;
            int aux = sliderParent.currentEmpty;
            sliderParent.currentEmpty = currentSlot;
            currentSlot = aux;
        }
        else
        {
            this.transform.position = initial.position;
        }
    }
}
