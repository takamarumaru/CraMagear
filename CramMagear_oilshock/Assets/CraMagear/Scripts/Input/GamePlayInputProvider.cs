using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInputProvider : InputProvider
{
   //左軸取得
   public override Vector2 GetAxisL()
    {
        return PlayerInputManager.Instance.GamePlay_GetAxisL();
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
