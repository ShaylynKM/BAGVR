using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InfoPanelVR : MonoBehaviour
{
    [SerializeField] private float offset = 1.1f;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Vector3 localScale = Vector3.one;
    [SerializeField] private GameObject infoUI;
    [SerializeField] private float displayDuration = 5.0f; 

    public bool isShown = false;
    private bool hasBeenShown = false;

    private XRGrabInteractable interactor;
    private Coroutine hideUICoroutine;

    private void Awake()
    {
        Debug.Log(gameObject.name);
        interactor = GetComponentInParent<XRGrabInteractable>();
        if (interactor != null)
        {
            interactor.selectEntered.AddListener(OnSelectEnter);
            Debug.Log("Event listener added to XRGrabInteractable.");
        }
    }

    private void OnDestroy()
    {
        if (interactor != null)
        {
            interactor.selectEntered.RemoveListener(OnSelectEnter);
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (hasBeenShown)
        {
            return;
        }

        if (isShown)
        {
            StopCoroutine(hideUICoroutine); 
            infoUI.SetActive(false); 
            isShown = false;
        }

        ShowInfo(); 
    }

    public void ShowInfo()
    {
        if (hasBeenShown) return;

        Debug.Log("DisplayInfoPanel triggered");

        infoUI.SetActive(true);
        isShown = true;
        hasBeenShown = true;

       // infoUI.transform.position = selectableTransform.position + Vector3.up * offset;
        
        hideUICoroutine = StartCoroutine(HideInfoAfterTime(displayDuration));
    }

    private IEnumerator HideInfoAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (infoUI.activeSelf)
        {
            infoUI.SetActive(false);
            isShown = false;
            Debug.Log("Info panel automatically hidden after timeout.");
        }
    }
}
