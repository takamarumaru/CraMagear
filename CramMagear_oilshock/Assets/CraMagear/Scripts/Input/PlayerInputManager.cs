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

    public enum ActionMapTypes
    {
        GamePlay,
        UI
    }

    //アクションマップの切り替え
    public void ChangeActionMap(ActionMapTypes ActionMapName)
    {
        _input.SwitchCurrentActionMap(ActionMapName.ToString());
    }


    //=============================================
    // ゲームプレイ関係
    //=============================================

    public Vector2 GamePlay_GetAxisL() => _actionMapGamePlay["Axis_L"].ReadValue<Vector2>();

    public Vector2 GamePlay_GetMouse() => _actionMapGamePlay["Camera"].ReadValue<Vector2>();

    public bool GamePlay_GetButtonAttack() => _actionMapGamePlay["Attack"].WasPerformedThisFrame();
    public bool GamePlay_GetButtonPressedAttack() => _actionMapGamePlay["Attack"].IsPressed();

    public bool GamePlay_GetButtonAim() => _actionMapGamePlay["Aim"].WasPerformedThisFrame();
    public bool GamePlay_GetButtonPressedAim() => _actionMapGamePlay["Aim"].IsPressed();

    public bool GamePlay_GetButtonJump() => _actionMapGamePlay["Jump"].WasPerformedThisFrame();

    public bool GamePlay_GetButtonArchitectureToggle() => _actionMapGamePlay["ArchitectureToggle"].WasPerformedThisFrame();

    //リストの左右切り替え
    public bool GamePlay_GetListSwitchingRight() => _actionMapGamePlay["ListSwitchingRight"].WasPerformedThisFrame();
    public bool GamePlay_GetListSwitchingLeft() => _actionMapGamePlay["ListSwitchingLeft"].WasPerformedThisFrame();


    public bool GamePlay_GetButtonMenu() => _actionMapUI["Menu"].WasPerformedThisFrame();

}
