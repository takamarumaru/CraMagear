using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GamePlayInputProvider : InputProvider
{
    [Header("[--Cinemachine参照--]")]
    [SerializeField] public CinemachineVirtualCamera _virtualCamera;
    [SerializeField] public CinemachineVirtualCamera _aimCamera;
    [SerializeField] GameObject _cameraLookPoint;

    [Header("[--カメラの上下制御--]")]
    [SerializeField] float _MaxCamRotateY = 0;
    [SerializeField] float _minCamRotateY = 0;

    [Header("[--レティクルのアクティブ非アクティブ制御--]")]
    [SerializeField] GameObject _reticleObject;

    [Header("[--建築--]")]
    [SerializeField] public ArchitectureCreator _architectureCreator;

    private void Awake()
    {
        Debug.Assert(_virtualCamera != null, "GamePlayInputProviderにCinemachineVirtualCameraが設定されていません。");
        Debug.Assert(_aimCamera != null, "GamePlayInputProviderにAimCameraが設定されていません。");
        Debug.Assert(_reticleObject != null, "GamePlayInputProviderにReticleObjectが設定されていません。");
        Debug.Assert(_architectureCreator != null, "GamePlayInputProviderにArchitectureCreatorが設定されていません。");
    }

    //AIからセットされるデータ
    public Vector2 AxisL { get; set; }
    public Vector2 AxisR { get; set; }
    public bool Attack { get; set; } = false;

    //左軸取得
    public override Vector2 GetAxisL() => AxisL;
    //攻撃状態かどうか
    public override bool GetAttackState() => Attack;

    public Vector2 PlayerMoveCameraDirection()
    {
        Vector2 axisL = PlayerInputManager.Instance.GamePlay_GetAxisL();
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        forward = cameraForward * forward.z + Camera.main.transform.right * forward.x;
        forward.Normalize();
        return new Vector2(forward.x, forward.z);
    }

    //右軸取得
    public override Vector2 GetAxisR() => Vector2.zero;

    public override Vector2 GetMouse()
    {
        return PlayerInputManager.Instance.GamePlay_GetMouse();
    }

    //攻撃ボタン
    public override bool GetButtonAttack()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAttack();
    }

    //Aim切り替えボタン
    public override bool GetButtonAim()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAim();
    }

    //長押し
    public override bool GetButtonPressedAim()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonPressedAim();
    }

    public override bool GetButtonArchitectureToggle()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonArchitectureToggle();
    }

    //ジャンプボタン
    public override bool GetButtonJump()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonJump();
    }

    public void PlayerRotation(Vector3 forward, float axisPower, Transform playerTrans)
    {

        //VirtualカメラよりAimカメラの方が優先度が低かったらカメラ方向に撃つ
        if (_aimCamera.Priority < _virtualCamera.Priority)
        {
            if (axisPower > 0.01f)
            {
                //入力した方向に回転
                Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                //キャラクターの回転にlerpして回転
                playerTrans.rotation = Quaternion.RotateTowards
                     (
                     transform.rotation,         //変化前の回転
                     rotation,                   //変化後の回転
                     720 * Time.deltaTime        //変化する角度
                     );
            }
        }
        else
        {

            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            //入力した方向に回転
            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
            //キャラクターの回転にlerpして回転
            playerTrans.rotation = rotation;

            //プレイヤーの回転
            playerTrans.Rotate(0.0f, GetMouse().x, 0.0f);

            //LookPointの回転
            _cameraLookPoint.transform.Rotate(-GetMouse().y, 0.0f, 0.0f);

            var localAngle = _cameraLookPoint.transform.localEulerAngles;

            //カメラ制御
            if (_MaxCamRotateY < localAngle.x && localAngle.x < 180)
                localAngle.x = _MaxCamRotateY;
            if (_minCamRotateY + 360 > localAngle.x && localAngle.x > 180)
                localAngle.x = _minCamRotateY + 360;

            //値を代入する
            _cameraLookPoint.transform.localEulerAngles = localAngle;

        }

    }


    //カメラ切り替え
    public void SelectUseCamera()
    {
        //カメラの優先順位
        int _EnablePriority = 10;
        int _DisablePriority = 9;

        //右クリック（カメラ切り替え）
        if (GetButtonPressedAim())
        {
            _virtualCamera.Priority = _DisablePriority;
            _aimCamera.Priority = _EnablePriority;

            _reticleObject.SetActive(true);
        }
        else
        {
            _virtualCamera.Priority = _EnablePriority;
            _aimCamera.Priority = _DisablePriority;
            _reticleObject.SetActive(false);
        }

    }
}
