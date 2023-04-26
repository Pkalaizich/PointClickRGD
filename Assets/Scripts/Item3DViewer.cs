using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject itemPrefab2;

    [SerializeField] private float rotationSpeed = 5f;

    private Quaternion initialRotation;

    void Start()
    {
        itemPrefab = null;
        //initialRotation = itemPrefab.transform.rotation;
    }

    private void OnDisable()
    {
        Destroy(itemPrefab2);
        itemPrefab2 =null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        float xRotation = eventData.delta.y * rotationSpeed;
        float yRotation = -eventData.delta.x * rotationSpeed;

        Quaternion deltaRotation = Quaternion.Euler(0f, yRotation, xRotation);
        itemPrefab2.transform.rotation = deltaRotation * itemPrefab2.transform.rotation;
    }

    public void SpawnObject(GameObject prefab)
    {
         itemPrefab2 = Instantiate(prefab, Vector3.one*100,new Quaternion(30,30,30,0));
        
    }
}
