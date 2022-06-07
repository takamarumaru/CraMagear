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
        //Debug.Log("Exit");
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
        //�d��
        //brain._velocity.y += brain._gravity * Time.deltaTime;

        //�U��
        if (input.GetButtonAttack())
        {
            Animator.SetTrigger("DoAttack");
        }

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        if (axisPower > 0.01f)
        {
            //���͂��������ɉ�]
            Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

            //�L�����N�^�[�̉�]��lerp���ĉ�]
            input.transform.rotation = Quaternion.RotateTowards
                 (
                 input.transform.rotation,   //�ω��O�̉�]
                 rotation,                   //�ω���̉�]
                 720 * Time.deltaTime        //�ω�����p�x
                 );
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

            //�L�����N�^�[�̉�]��lerp���ĉ�]
            input.transform.rotation = Quaternion.RotateTowards
                 (
                 input.transform.rotation,   //�ω��O�̉�]
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
