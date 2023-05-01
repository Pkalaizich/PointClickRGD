using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderParent : MonoBehaviour
{
    public List<Transform> Positions;
    public int currentEmpty = 8;
    public bool allPiecesInPlace = false;
    [SerializeField] private List<EventScriptable> requiredEvents;

    [SerializeField] private EventScriptable eventToComplete;

    [SerializeField] private string requiredEventsNotMet;
    [SerializeField] private string requiredEventsMet;

    [SerializeField] private string solvedMessage;

    [SerializeField] private List<SliderPiece> _Pieces;

    private bool validSlider = false;
    private bool active =false;

    private void OnEnable()
    {
        while(!validSlider)
        {
            ShufflePieces();
            validSlider = !SliderIsSolved();
        }        
        _Pieces[_Pieces.Count-1].gameObject.SetActive(false);
        active = true;
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


    public void ShufflePieces()
    {
        bool solvable = false;
        while(!solvable)
        {
            _Pieces.Shuffle();
            for (int i = 0; i < _Pieces.Count; i++)
            {
                _Pieces[i].SetCurrentSlot(i);
                _Pieces[i].gameObject.transform.position = Positions[i].position;
            }
            solvable = IsSolvable();
        }
    }

    public bool IsSolvable()
    {
        int inversions = 0;
        for(int i =0; i < _Pieces.Count-1;i++)
        {
            for(int j = i+1;j<_Pieces.Count;j++)
            {
                if (_Pieces[i].targetPosition > _Pieces[j].targetPosition)
                {
                    inversions++;
                }
            }
        }
        if(inversions%2 == 0)
        {
            return true;
        }        
        return false;
    }

    public bool SliderIsSolved()
    {
        for (int i=0; i< _Pieces.Count;i++)
        {
            if (_Pieces[i].targetPosition != _Pieces[i].currentSlot)
            {
                return false;
            }
        }
        if(active)
        {
            SetPiecesInteractable(false);
            UIController.Instance.SetDialog(solvedMessage);
            GamePlayEvents.Instance.SetEventCompleted(eventToComplete.EventIndex);
            active = false;
        }        
        return true;
    }

    
}
