using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
    }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void OnDestroy() {
        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVectorNormalised() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetBindingText(Binding binding) {
        switch (binding) {
            default:
            case Binding.Move_Up:
            return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
            return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
            return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
            return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }
        public void RebindBinding(Binding binding, Action onActionRebound) {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding) {
            default:
            case Binding.Move_Up:
            inputAction = playerInputActions.Player.Move;
            bindingIndex = 1;
            break;

            case Binding.Move_Down:
            inputAction = playerInputActions.Player.Move;
            bindingIndex = 2;
            break;

            case Binding.Move_Left:
            inputAction = playerInputActions.Player.Move;
            bindingIndex = 3;
            break;

            case Binding.Move_Right:
            inputAction = playerInputActions.Player.Move;
            bindingIndex = 4;
            break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => { 
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
        }).Start();
        }
}
