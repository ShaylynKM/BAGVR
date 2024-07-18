using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject startUI; 
    [SerializeField] private float displayDuration = 5.0f; 

    private void Start()
    {
        ShowStartUI();
    }

    
    private void ShowStartUI()
    {
        if (startUI != null)
        {
            startUI.SetActive(true); 
            StartCoroutine(HideStartUIAfterTime()); 
        }
    }

    private IEnumerator HideStartUIAfterTime()
    {
        yield return new WaitForSeconds(displayDuration); 
        if (startUI.activeSelf)
        {
            startUI.SetActive(false); 
        }
    }
}
