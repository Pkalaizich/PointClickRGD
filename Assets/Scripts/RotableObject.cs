using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotableObject : MonoBehaviour
{
    private ItemCamera itemCamera;
    //private Camera rayCastCamera;
    [SerializeField] private List<GameObject> referencePoints;

    [SerializeField] private string requiredEventsNotMet;
    [SerializeField] private string requiredEventsMet;
    [SerializeField] private string eventCompleted;

    [SerializeField] private List<EventScriptable> requiredEvents;
    [SerializeField] private EventScriptable eventToComplete;
    

    private void OnEnable()
    {
        itemCamera = FindObjectOfType<ItemCamera>();
        //rayCastCamera = itemCamera.GetComponent<Camera>();
        StartCoroutine(ContinuousCheck());
        UIController.Instance.AddListenerCompletedEventToInspect(eventToComplete.EventIndex);
        if(GamePlayEvents.Instance.RequiredEvents(requiredEvents))
        {
            UIController.Instance.AddListenerMessageToInspect(requiredEventsMet);            
        }
        else
        {
            UIController.Instance.AddListenerMessageToInspect(requiredEventsNotMet);
        }
    }

    private void OnDestroy()
    {
        UIController.Instance.SetInspectorVisibility(false);
        UIController.Instance.RemoveInspectListeners();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P)) { CheckVisibility(); }
    }

    private IEnumerator ContinuousCheck()
    {
        while(!eventToComplete.Completed)
        {
            CheckVisibility();
            yield return new WaitForSeconds(0.3f);
        }        
    }

    private void CheckVisibility()
    {        
        // Set the maximum distance of the raycast
        float maxDistance = 15f;

        // Create a ray from the camera to the object

        foreach(GameObject objectToCheck in referencePoints) 
        {
            Ray ray = new Ray(itemCamera.transform.position, objectToCheck.transform.position - itemCamera.transform.position);

            // Check if the ray hits an object that's closer than the object we're checking visibility for
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance))
            {
                if (hitInfo.collider.gameObject != objectToCheck)
                {
                    Debug.Log("Object is obstructed");
                    UIController.Instance.SetInspectorVisibility(false);
                    return;
                }
            }            
        }
        Debug.Log("All Objects Visible");
        UIController.Instance.SetInspectorVisibility(true);
        
        itemCamera.shakeDuration = 0.3f;
        
    }
}
