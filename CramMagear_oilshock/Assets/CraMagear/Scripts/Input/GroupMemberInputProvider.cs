using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMemberInputProvider : InputProvider
{
    //�����擾
    public override Vector2 GetAxisL()
    {
        return new Vector3();
    }

    //�E���擾
    public override Vector2 GetAxisR() => Vector2.zero;

    //�U���{�^��
    public override bool GetButtonAttack()
    {
        return false;
    }
}
