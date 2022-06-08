using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    //�����擾
    public virtual Vector2 GetAxisL() => Vector2.zero;
    //�E���擾
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //��]
    public virtual Quaternion GetRotation() => Quaternion.identity;

    //�}�E�X�ړ���
    public virtual Vector2 GetMouse() => Vector2.zero;

    //�U���{�^��
    public virtual bool GetButtonAttack() => false;

    //Aim�؂�ւ��{�^��
    public virtual bool GetButtonAim() => false;
    public virtual bool GetButtonPressedAim() => false;

    //���z�{�^��
    public virtual bool GetButtonArchitectureToggle() => false;

    //�W�����v�{�^��
    public virtual bool GetButtonJump() => false;

    //�L�����N�^�[��]
    public void RotateAxis(Vector3 forward,Transform trans)
    {

        //���͂��������ɉ�]
        Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

        //�L�����N�^�[�̉�]��lerp���ĉ�]
        trans.rotation = Quaternion.RotateTowards
             (
             trans.rotation,             //�ω��O�̉�]
             rotation,                   //�ω���̉�]
             720 * Time.deltaTime        //�ω�����p�x
             );

    }
}
