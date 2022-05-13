using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMemberInputProvider : InputProvider
{
    //¶²æ“¾
    public override Vector2 GetAxisL()
    {
        return new Vector3();
    }

    //‰E²æ“¾
    public override Vector2 GetAxisR() => Vector2.zero;

    //UŒ‚ƒ{ƒ^ƒ“
    public override bool GetButtonAttack()
    {
        return false;
    }
}
