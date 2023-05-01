using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Singleton
    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIController>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private Button rightBtn;
    [SerializeField] private Button leftBtn;
    private Player player;

    [SerializeField] private GameObject itemViewer;
    [SerializeField] private GameObject darkBckg;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button inspectButton;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject messageBox;

    private void Awake()
    {
        
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rightBtn.onClick.AddListener(() => StartCoroutine(BeginRotation(true)));
        leftBtn.onClick.AddListener(() => StartCoroutine(BeginRotation(false)));
        backBtn.onClick.AddListener(() => {
            FindObjectOfType<Player>().BackToNormal();
            backBtn.gameObject.SetActive(false);
            StartCoroutine(DelayedReactivate());
            FindObjectOfType<SliderPedestal>().enabled= true;
            FindObjectOfType<SliderPedestal>().EnableComponents(true);
            inspectButton.onClick.RemoveAllListeners();
            inspectButton.gameObject.SetActive(false);
        });
    }

    public void ShowBackBtn()
    {
        backBtn.gameObject.SetActive(true);
    }

    private IEnumerator DelayedReactivate()
    {
        yield return new WaitForSeconds(1.5f);
        ControlRotationButtons(true);
    }

    public IEnumerator BeginRotation(bool right)
    {
        player.Rotation(right);
        rightBtn.interactable = false;
        leftBtn.interactable= false;
        yield return new WaitForSeconds(1f);
        rightBtn.interactable = true;
        leftBtn.interactable = true;

    }

    public void Open3Dviewer()
    {
        darkBckg.SetActive(true);
        itemViewer.SetActive(true);
        closeBtn.gameObject.SetActive(true);
    }

    public void ControlRotationButtons(bool show)
    {
        rightBtn.gameObject.SetActive(show);
        leftBtn.gameObject.SetActive(show);
    }

    public void SetDialog(string message)
    {
        messageText.text = message;
        StartCoroutine(ShowDialog());
    }


    private IEnumerator ShowDialog()
    {
        messageBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        messageBox.gameObject.SetActive(false);
    }

    #region Inspect Button Methods
    public void AddListenerMessageToInspect(string message)
    {
        inspectButton.onClick.AddListener(() => {
            SetDialog(message);
            inspectButton.gameObject.SetActive(false);
        });
    }

    public void AddListenerCompletedEventToInspect(int eventIndex)
    {
        inspectButton.onClick.AddListener(()=> {
            GamePlayEvents.Instance.SetEventCompleted(eventIndex);            
        });
        //FindObjectOfType<SoundController>().PlayDiscover();
    }

    public void AddAnAction(Action action)
    {
        inspectButton.onClick.AddListener(() => action.Invoke());
    }

    public void RemoveInspectListeners()
    {
        inspectButton.onClick.RemoveAllListeners();
    }
   

    public void SetInspectorVisibility(bool visible)
    {
        inspectButton.gameObject.SetActive(visible);
    }

    #endregion
}
