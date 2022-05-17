using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInputProvider : InputProvider
{
   //�����擾
   public override Vector2 GetAxisL()
    {
        Vector2 axisL = PlayerInputManager.Instance.GamePlay_GetAxisL();
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        forward = cameraForward * forward.z + Camera.main.transform.right * forward.x;
        forward.Normalize();
        return new Vector2(forward.x, forward.z);
    }

    //�E���擾
    public override Vector2 GetAxisR() => Vector2.zero;

    //�U���{�^��
    public override bool GetButtonAttack()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAttack();
    }

    //�W�����v�{�^��
    public override bool GetButtonJump()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonJump();
    }

}
