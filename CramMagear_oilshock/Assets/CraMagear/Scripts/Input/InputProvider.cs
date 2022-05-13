using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    //�����擾
    public virtual Vector2 GetAxisL() => Vector2.zero;
    //�E���擾
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //�U���{�^��
    public virtual bool GetButtonAttack() => false;
}