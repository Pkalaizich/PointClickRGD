using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEvents : MonoBehaviour
{
    //private static GamePlayEvents instance;
    //public static GamePlayEvents Instance { get => instance; }
    
    #region Singleton
    private static GamePlayEvents _instance;
    public static GamePlayEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GamePlayEvents>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    #endregion
    [SerializeField] private List<EventScriptable> events;

    private void Awake()
    {
        foreach(EventScriptable _event in events)
        {
            _event.Completed = false;
        }
    }

    public void SetEventCompleted(int eventIndex)
    {
        EventScriptable ToModify = events.Find(x => x.EventIndex == eventIndex);
        ToModify.Completed =true;
    }

    public bool RequiredEvents (List<EventScriptable> RequiredEvents)
    {
        foreach(EventScriptable Event in RequiredEvents)
        {
            EventScriptable ToCheck = events.Find(x => x.EventIndex == Event.EventIndex);
            if (ToCheck.Completed == false)
            {
                return false;
            }
        }
        return true;
    }
}
