using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Event")]
public class EventScriptable : ScriptableObject
{
    public int EventIndex;
    public bool Completed = false;
}
