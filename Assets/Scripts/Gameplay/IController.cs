using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IController : MonoBehaviour
{
    protected Vector2 MoveValue;
    
    void Start()
    {
        SetUpInput();
    }

    private void OnEnable()
    {
        SetUpInput();
    }

    private void OnDisable()
    {
        CancelInput();
    }

    public void SetUpInput()
    {
        CancelInput();
        
        if (InputMgr.IsValid)
        {
            InputMgr.Instance.OnMoveAction += OnMove;
            InputMgr.Instance.OnShootAction += OnShoot;
            InputMgr.Instance.OnShootCancelAction += OnShootCancel;
        }
    }

    public void CancelInput()
    {
        if (InputMgr.IsValid)
        {
            InputMgr.Instance.OnMoveAction -= OnMove;
            InputMgr.Instance.OnShootAction -= OnShoot;
            InputMgr.Instance.OnShootCancelAction -= OnShootCancel;
        }
    }
    
    private void OnMove(Vector2 arg0)
    {
        MoveValue = arg0;
    }
    
    private void OnShoot()
    {
        
    }
    
    private void OnShootCancel()
    {
        
    }
}
