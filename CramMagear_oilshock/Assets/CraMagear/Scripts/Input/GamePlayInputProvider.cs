using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInputProvider : InputProvider
{
   //�����擾
   public override Vector2 GetAxisL()
    {
        return PlayerInputManager.Instance.GamePlay_GetAxisL();
    }

    //�E���擾
    public override Vector2 GetAxisR() => Vector2.zero;

    //�U���{�^��
    public override bool GetButtonAttack()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAttack();
    }

}
