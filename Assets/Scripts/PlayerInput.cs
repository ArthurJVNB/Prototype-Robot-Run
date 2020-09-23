using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action OnDash;
    public Action OnJump;

    public float Vertical { get { return GetVertical(); } }
    public float Horizontal { get { return GetHorizontal(); } }
    public float VerticalRaw { get { return GetVerticalRaw(); } }
    public float HorizontalRaw { get { return GetHorizontalRaw(); } }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
            OnDash?.Invoke();

        if (Input.GetButtonDown("Jump"))
            OnJump?.Invoke();
    }

    private float GetVertical()
    {
        return Input.GetAxis("Vertical");
    }
    private float GetHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    private float GetVerticalRaw()
    {
        return Input.GetAxisRaw("Vertical");
    }

    private float GetHorizontalRaw()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
