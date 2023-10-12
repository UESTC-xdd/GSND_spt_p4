using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectInteractable : IInteractable
{
    public bool IsInspecting { get; set; }

    public DialogueLine DialogLine;

    public override void OnInteract()
    {
        base.OnInteract();
        Interactable = false;
        GameManager.Instance.PlayerInteractor.InspectObj(transform);

        EventMgr.OnInteract -= OnEBtn;
        EventMgr.OnInteract += OnEBtn;
    }

    private void OnEBtn()
    {
        GameManager.Instance.PlayerInteractor.ReturnInspectObj();
        EventMgr.OnInteract -= OnEBtn;
        Interactable = true;
    }
}
