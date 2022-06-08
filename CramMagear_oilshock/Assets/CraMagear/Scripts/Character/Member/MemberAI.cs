using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel���AI�X�e�[�g
[System.Serializable]
public class MemberAIStateIdle : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<GroupMemberInputProvider>();

        Vector2 axisL = input.GetAxisL();

        if (axisL.magnitude > 0.1f)
        {
            Animator.SetBool("IsMoving", true);
        }

        //�U��
        if (input.GetButtonAttack())
        {
            Animator.SetTrigger("DoAttack");
        }

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        if (axisPower > 0.01f)
        {
            input.RotateAxis(forward, StateMgr.transform);
        }

        //�W�����v
        if (input.GetButtonJump())
        {
            Animator.SetBool("IsJump", true);
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}

//�ǐՏ��AI�X�e�[�g
[System.Serializable]
public class MemberAIStateChase : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        var input = StateMgr.GetComponent<GroupMemberInputProvider>();

        //����{�^��
        if (input.GetButtonAttack())
        {

            Animator.SetTrigger("DoAttack");

            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            //���͂��������ɉ�]
            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
            //�L�����N�^�[�̉�]��lerp���ĉ�]
            StateMgr.CharaBrain.transform.rotation = rotation;

            return;
        }

        Vector2 axisL = input.GetAxisL();
        if (axisL.magnitude < 0.1f)
        {
            Animator.SetBool("IsMoving", false);
            return;
        }

        //�W�����v
        if (input.GetButtonJump())
        {
            Animator.SetBool("IsJump", true);
            return;
        }

        Animator.SetFloat("MoveSpeed", axisL.magnitude);

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);

        forward.Normalize();

        //--------------
        //����
        //--------------
        if (axisPower > 0.01f)
        {
            input.RotateAxis(forward, StateMgr.transform);
        }

        //--------------
        //�ړ�
        //--------------
        input.AxisL = new Vector2(forward.x, forward.z);
    }

    //�Œ�t���[�����[�g�œ����X�V
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}

//�U�����AI�X�e�[�g
[System.Serializable]
public class MemberAIStateAttack : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}

[System.Serializable]
public class MemberAIStateJump : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}
