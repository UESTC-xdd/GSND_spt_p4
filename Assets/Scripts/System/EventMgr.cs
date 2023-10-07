using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventMgr
{
    public static UnityAction<float> OnTickEvt;

    public static UnityAction<Vector2> OnLeftStickEvt;

    public static UnityAction<string> OnInventoryItemUsed;
}
