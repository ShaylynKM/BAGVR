using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : Singleton<TutorialManager>
{
    public UnityEvent BreadGrabbed;
    public UnityEvent CheeseAdded;
    public UnityEvent HamAdded;
    public UnityEvent MustardAdded;
    public UnityEvent TopBreadAdded;
    public UnityEvent KidHitWithSandwich;

    public UnityEvent PauseGame;
    public UnityEvent ResumeGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
