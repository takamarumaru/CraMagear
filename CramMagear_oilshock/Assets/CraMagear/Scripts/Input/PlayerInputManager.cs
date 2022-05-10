using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    //シングルトン
    static public PlayerInputManager Instance { get; private set; }

    UnityEngine.InputSystem.PlayerInput _input;

    InputActionMap _actionMapGamePlay;
    InputActionMap _actionMapUI;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;

        TryGetComponent(out _input);
        _actionMapGamePlay = _input.actions.FindActionMap("GamePlay");
        _actionMapUI = _input.actions.FindActionMap("UI");
    }

    //=============================================
    // ゲームプレイ関係
    //=============================================

    public Vector2 GamePlay_GetAxisL() => _actionMapGamePlay["Axis_L"].ReadValue<Vector2>();

    public bool GamePlay_GetButtonAttack() => _actionMapGamePlay["Attack"].WasPerformedThisFrame();

    public bool GamePlay_GetButtonMenu() => _actionMapUI["Menu"].WasPerformedThisFrame();

}
