using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        TutorialManager.Instance.KidHitWithSandwich.AddListener(StartKidTutorial);
        TutorialManager.Instance.BreadGrabbed.AddListener(StartTutorial);      
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        text.text = "complete all tasks on the clipboard to continue";
        yield return null;  
    }

    public void StartKidTutorial()
    {
        StartCoroutine(KidTutorial());
    }
    IEnumerator KidTutorial()
    {
        text.text = "The kid will always be approaching";
        yield return new WaitForSeconds(5);
        text.text = "They will also get faster over time";
        yield return new WaitForSeconds(5);
        text.text = "Good luck";
        yield return new WaitForSeconds(3);
        TutorialManager.Instance.EndTutorial.Invoke();
        transform.parent.gameObject.SetActive(false);
        yield return null;
    }
}
