using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    private void OnBecameVisible()
    {
        Debug.Log("POI visible");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("POI invisible");
    }
}
