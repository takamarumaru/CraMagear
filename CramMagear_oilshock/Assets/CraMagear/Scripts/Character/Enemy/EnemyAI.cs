using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel���AI�X�e�[�g
[System.Serializable]
public class EnemyAIStateIdle : GameStateMachine.StateNodeBase
{
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<EnemyInputProvider>();

        Vector2 axisL = input.SearchTargetDirection();

        if (axisL.magnitude > 0.1f)
        {
            Animator.SetBool("IsMoving", true);
        }

        //�U��
        if (input.GetButtonAttack())
        {
            Animator.SetTrigger("DoAttack");

            return;
        }

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        if (axisPower > 0.01f)
        {
            input.RotateAxis(forward, StateMgr.CharaBrain.transform);
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

    }
}

//�ǐՏ��AI�X�e�[�g
[System.Serializable]
public class EnemyAIStateChase : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<EnemyInputProvider>();

        //����{�^��
        if (input.GetButtonAttack())
        {
            Animator.SetTrigger("DoAttack");

            return;
        }

        Vector2 axisL = input.SearchTargetDirection();
        if (axisL.magnitude < 0.1f)
        {
            Animator.SetBool("IsMoving", false);
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
            //���͂��������ɉ�]
            Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

            //�ω��O�̉�]
            Quaternion prevRotation = StateMgr.CharaBrain.transform.rotation;

            //�L�����N�^�[�̉�]��lerp���ĉ�]
            StateMgr.CharaBrain.transform.rotation = Quaternion.RotateTowards
                         (
                         prevRotation,               //�ω��O�̉�]
                         rotation,                   //�ω���̉�]
                         720 * Time.deltaTime        //�ω�����p�x
                         );
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
public class EnemyAIStateAttack : GameStateMachine.StateNodeBase
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
