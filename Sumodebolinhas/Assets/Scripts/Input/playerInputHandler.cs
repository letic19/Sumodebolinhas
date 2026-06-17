using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public enum PlayerType
    {
        Player1,
        Player2
    }

    [SerializeField]
    private PlayerType playerType;

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

        if (playerType == PlayerType.Player1)
        {
            controls.Player1.Move.performed += MovePerformed;
            controls.Player1.Move.canceled += MovePerformed;
            controls.Player1.Push.performed += PushPerformed;
        }
        else
        {
            controls.Player2.Move.performed += MovePerformed;
            controls.Player2.Move.canceled += MovePerformed;
            controls.Player2.Push.performed += PushPerformed;
        }
    }

    private void OnDisable()
    {
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