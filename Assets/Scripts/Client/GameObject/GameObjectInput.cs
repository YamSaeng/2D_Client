using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectInput : MonoBehaviour
{
    private Camera _MainCamera;    
    private bool _IsDefaultAttack = false;

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    [field: SerializeField]
    public UnityEvent OnDefaultAttackPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnDefaultAttackReleased { get; set; }

    [field: SerializeField]
    public UnityEvent OnInventroyUIOpen { get; set; }

    [field: SerializeField]
    public UnityEvent OnEquipmentUIOpen { get; set; }

    [field: SerializeField]
    public UnityEvent OnSkillUIOpen { get; set; }

    private void Awake()
    {
        _MainCamera = Camera.main;
    }

    void Update()
    {
        GetUIKeyInput();
        GetPointerInput();
        GetMovementInput();
        GetDefaultAttackInput();
    }

    private void GetUIKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OnInventroyUIOpen?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            OnEquipmentUIOpen?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            OnSkillUIOpen?.Invoke();
        }
    }

    private void GetDefaultAttackInput()
    {
        if (Input.GetAxisRaw("DefaultAttack") > 0)
        {            
            if (_IsDefaultAttack == false)
            {
                _IsDefaultAttack = true;
                OnDefaultAttackPressed?.Invoke();
            }
        }
        else
        {
            if (_IsDefaultAttack == true)
            {
                _IsDefaultAttack = false;
                OnDefaultAttackReleased?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        if (Input.GetMouseButton(1) == true)
        {
            Vector3 ScreenMousePosition = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
            OnPointerPositionChange?.Invoke(ScreenMousePosition);
        }        
    }

    private void GetMovementInput()
    {
        Vector2 Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMovementKeyPressed?.Invoke(Direction);
    }
}
