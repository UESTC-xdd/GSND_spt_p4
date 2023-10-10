using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPInteractor : MonoBehaviour
{
    public IInteractable CurInteractable;
    public LayerMask InteractorLayer;

    private void Update()
    {
        DetectInteractableObj();
    }

    private void DetectInteractableObj()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
            out RaycastHit hit, float.MaxValue, InteractorLayer,QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                //In Interaction Range
                if(interactable.Interactable && interactable.IsPlayerInRange)
                {
                    if (CurInteractable == null)
                    {
                        CurInteractable = interactable;
                        CurInteractable.CanInteract = true;
                        UIMgr.Instance.CenterPoint.SetHandEnabled(true);
                    }
                    else
                    {
                        if (CurInteractable == interactable)
                        {
                            CurInteractable.CanInteract = true;
                            UIMgr.Instance.CenterPoint.SetHandEnabled(true);
                        }
                        else
                        {
                            CurInteractable.CanInteract = false;
                            CurInteractable = interactable;
                            CurInteractable.CanInteract = true;
                            UIMgr.Instance.CenterPoint.SetHandEnabled(true);
                        }
                    }
                }
                else
                {
                    ClearCurInteractable();
                }
            }
            else
            {
                ClearCurInteractable();
            }
        }
    }

    public void ClearCurInteractable()
    {
        if (CurInteractable != null)
        {
            CurInteractable.CanInteract = false;
            CurInteractable = null;
            UIMgr.Instance.CenterPoint.SetHandEnabled(false);
        }
    }

    public void OnInteract()
    {
        if (CurInteractable != null)
        {
            CurInteractable.OnInteract();
        }
    }
}
