using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInputProvider : InputProvider
{
   //¶²æ“¾
   public override Vector2 GetAxisL()
    {
        return PlayerInputManager.Instance.GamePlay_GetAxisL();
    }

    //‰E²æ“¾
    public override Vector2 GetAxisR() => Vector2.zero;

    //UŒ‚ƒ{ƒ^ƒ“
    public override bool GetButtonAttack()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAttack();
    }

}
