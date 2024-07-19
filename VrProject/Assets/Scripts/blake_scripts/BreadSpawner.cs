using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BreadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _breadPrefab;

    XRDirectInteractor _interactor;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ajhd");
        if (other.GetComponent<XRDirectInteractor>())
        {
            Debug.Log("ajhd");
            _interactor = other.GetComponent<XRDirectInteractor>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<XRDirectInteractor>())
        {
            _interactor = null;
        }
    }

    private void Update()
    {
        if(_interactor != null)
        {
            if (_interactor.GetComponentInParent<ActionBasedController>().selectAction.action.triggered)
            {
                Debug.Log("ajhd");
                _interactor.GetComponentInParent<XRInteractionManager>().ForceSelect(_interactor, _breadPrefab.GetComponent<XRGrabInteractable>());
            }
        }
   
    }



}
