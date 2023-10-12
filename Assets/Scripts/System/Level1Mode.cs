using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Mode : LevelSingleton<Level1Mode>
{
    [Header("First Task")]
    public Outline CalenderOutline;

    [Header("Second Task")]
    public Outline ToDoListOutline;

    [Header("Third Task")]
    public Outline[] AllCleanOutline;
    public List<IInteractable> AllCleanOutInteractables = new List<IInteractable>();

    private void Start()
    {
        SetUpFirstTask();
    }

    public void ChangeToGameMode()
    {
        GameManager.Instance.ChangeToGameMode();
    }

    public void SetUpFirstTask()
    {
        CalenderOutline.enabled = true;
    }

    public void FinishFirstTask()
    {
        Debug.Log("Fist Task Finished");
        SetUpSecondTask();
    }
        

    public void SetUpSecondTask()
    {
        ToDoListOutline.enabled = true;
    }

    public void FinishSecondTask()
    {
        Debug.Log("Second Task Finished");
        SetUpThirdTask();
    }

    public void SetUpThirdTask()
    {
        foreach (var outline in AllCleanOutline)
        {
            outline.enabled = true;
        }

        foreach (var interactable in AllCleanOutInteractables)
        {
            interactable.Interactable = true;
        }
    }

    public void FinishedOneClean(IInteractable thisInteractable)
    {
        if(AllCleanOutInteractables.Contains(thisInteractable))
        {
            AllCleanOutInteractables.Remove(thisInteractable);
            if(AllCleanOutInteractables.Count<=0)
            {
                FinishThirdTask();
            }
        }
        thisInteractable.gameObject.SetActive(false);
    }

    public void FinishThirdTask()
    {
        Debug.Log("Third Task Finished");
    }
}
