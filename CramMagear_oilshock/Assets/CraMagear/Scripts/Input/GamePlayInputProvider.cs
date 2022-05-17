using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInputProvider : InputProvider
{
   //左軸取得
   public override Vector2 GetAxisL()
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

    //攻撃ボタン
    public override bool GetButtonAttack()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAttack();
    }

    //ジャンプボタン
    public override bool GetButtonJump()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonJump();
    }

}
