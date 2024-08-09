using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipBoard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Instruction1;
    [SerializeField] private SpriteRenderer Instruction2;
    [SerializeField] private SpriteRenderer[] SubInstruction2;
    [SerializeField] private SpriteRenderer Instruction3;
    [SerializeField] private SpriteRenderer Instruction4;
    private TutorialManager tutorial;

    private void Awake()
    {
        tutorial = TutorialManager.Instance;
        Instruction1.enabled = false;
        Instruction2.enabled = false;
        Instruction3.enabled = false;
        Instruction4.enabled = false;
        foreach(SpriteRenderer sp in SubInstruction2)
        {
            sp.enabled = false;
        }
    }

    private void Start()
    {
        tutorial.BreadGrabbed.AddListener(BreadComplete);
        tutorial.HamAdded.AddListener(HamAdded);
        tutorial.CheeseAdded.AddListener(CheeseAdded);
        tutorial.MustardAdded.AddListener(MustardAdded);
        tutorial.TopBreadAdded.AddListener(SandwhichMade);
        tutorial.KidHitWithSandwich.AddListener(ChildHit);
    }

    private void Update()
    {
        CheckIngredients();
      
    }
   
    public void BreadComplete()
    {
        Instruction1.enabled = true;
    }

    public void HamAdded()
    {
        SubInstruction2[0].enabled = true;
    }
    public void CheeseAdded()
    {
        SubInstruction2[1].enabled = true;
    }
    public void MustardAdded()
    {
        SubInstruction2[2].enabled = true;
    }
    private void CheckIngredients()
    {
        int count = 0;
        foreach(SpriteRenderer sp in SubInstruction2)
        {
            if(sp.enabled)
            {
                count++;
            }
        }
        if(count >= 3)
        {
            Instruction2.enabled = true;
        }
    }

    public void SandwhichMade()
    {
        Instruction3.enabled = true;
    }

    public void ChildHit()
    {
        Instruction4.enabled = true;
    }
}
