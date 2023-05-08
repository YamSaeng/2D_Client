using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NetworkGameObjectInput : MonoBehaviour
{
    [HideInInspector]
    public Vector2 _LookAtPosition;

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    private void Update()
    {
        GetPointerInput();   
    }

    private void GetPointerInput()
    {
        OnPointerPositionChange?.Invoke(_LookAtPosition);
    }
}
