using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel���AI�X�e�[�g
[System.Serializable]
public class TurretChaseEnemyAIStateIdle : GameStateMachine.StateNodeBase
{
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<EnemyInputProvider>();

        var collisionInfo = input.GetComponentInChildren<TargetSearcher>();
        if(collisionInfo.GetClosestTarget().HasValue)
        {
            input.TargetTransform = collisionInfo.GetClosestTarget().Value.MainObjectParameter.gameObject.transform;
        }
        Vector2 axisL = input.SearchTargetDirection();

        if (axisL.magnitude > 0.1f)
        {
            Animator.SetBool("IsMoving", true);
        }
        Debug.Log("EnemyAI");

        //�d�����ǂ���
        if (StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", true);
            Debug.Log("�X�^��");
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
public class TurretChaseEnemyAIStateChase : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<EnemyInputProvider>();

        var collisionInfo = input.GetComponentInChildren<TargetSearcher>();
        if (collisionInfo.GetClosestTarget().HasValue)
        {
            MainObjectParameter targetParam = collisionInfo.GetClosestTarget().Value.MainObjectParameter;
            if (targetParam)
            {
                input.TargetTransform = targetParam.gameObject.transform;
            }
        }
        Vector2 axisL = input.SearchTargetDirection();

        //�d�����ǂ���
        if (StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", true);
            Debug.Log("�X�^��");
            return;
        }

        //����{�^��
        if (input.GetButtonAttack())
        {
            Animator.SetTrigger("DoAttack");

            return;
        }

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
public class TurretChaseEnemyAIStateAttack : GameStateMachine.StateNodeBase
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

        //�d�����ǂ���
        if (StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", true);
        }
    }
}

//�d�����AI�X�e�[�g
[System.Serializable]
public class TurretChaseEnemyAIElectricShock : GameStateMachine.StateNodeBase
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

        //�d�����ǂ���
        if (!StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", false);
        }
    }
}
