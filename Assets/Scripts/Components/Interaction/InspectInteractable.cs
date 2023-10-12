using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectInteractable : IInteractable
{
    public bool IsInspecting { get; set; }

    public DialogueLine DialogLine;
    public DialogType CurDialogType;

    public override void OnInteract()
    {
        base.OnInteract();
        Interactable = false;
        GameManager.Instance.PlayerInteractor.InspectObj(transform);
        UIMgr.Instance.DialogC.StartDialogue(DialogLine, CurDialogType);

        EventMgr.OnInteract -= OnEBtn;
        EventMgr.OnInteract += OnEBtn;
    }

    private void OnEBtn()
    {
        UIMgr.Instance.DialogC.StopDialog();
        GameManager.Instance.PlayerInteractor.ReturnInspectObj();
        EventMgr.OnInteract -= OnEBtn;
        Interactable = true;
    }
}
