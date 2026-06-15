using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls controls;

    public event Action<Vector2> OnMove;

    public event Action OnPush;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player1.Move.performed += MovePerformed;
        controls.Player1.Move.canceled += MovePerformed;

        controls.Player1.Push.performed += PushPerformed;
    }

    private void OnDisable()
    {
        controls.Player1.Move.performed -= MovePerformed;
        controls.Player1.Move.canceled -= MovePerformed;

        controls.Player1.Push.performed -= PushPerformed;

        controls.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    private void PushPerformed(InputAction.CallbackContext context)
    {
        OnPush?.Invoke();
    }
}