using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public bool checkLeftClick() {
        if (Input.GetMouseButtonDown(0)) {
            return true;
        }
        return false;
    }

    public bool checkRightClick() {
        if (Input.GetMouseButtonDown(1)) {
            return true;
        }
        return false;
    }

    public Vector2 GetMovementVectorNormalised() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
