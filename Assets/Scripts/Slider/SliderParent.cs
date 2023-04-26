using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderParent : MonoBehaviour
{
    public List<Transform> Positions;
    public int currentEmpty = 8;
    public bool allPiecesInPlace = false;
    [SerializeField] private List<EventScriptable> requiredEvents;

    [SerializeField] private string requiredEventsNotMet;
    [SerializeField] private string requiredEventsMet;

    [SerializeField] private List<SliderPiece> _Pieces;

    private void OnEnable()
    {
        _Pieces[_Pieces.Count-1].gameObject.SetActive(false);
    }

    
    public Transform GetSlotPosition(int i)
    {
        return Positions[i];
    }

    public void SetCurrentEmpty(int i)
    {
        currentEmpty= i;
    }

    public void SetPiecesInteractable(bool interactable)
    {
        foreach(SliderPiece piece in _Pieces)
        {
            piece.enabled = (interactable&&allPiecesInPlace);
        }
    }

    public void SetButton()
    {
        if(GamePlayEvents.Instance.RequiredEvents(requiredEvents))
        {
            UIController.Instance.AddListenerMessageToInspect(requiredEventsMet);
            UIController.Instance.AddAnAction(ShowMissingPiece);
        }
        else
        {
            UIController.Instance.AddListenerMessageToInspect(requiredEventsNotMet);
        }
    }

    public void ShowMissingPiece()
    {
        _Pieces[_Pieces.Count - 1].gameObject.SetActive(true);
        allPiecesInPlace = true;
        SetPiecesInteractable(true);
    }
}
