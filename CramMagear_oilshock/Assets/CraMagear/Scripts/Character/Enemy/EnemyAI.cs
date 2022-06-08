using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel状態AIステート
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

        //攻撃
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
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

    }
}

//追跡状態AIステート
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

        //決定ボタン
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
        //向き
        //--------------
        if (axisPower > 0.01f)
        {
            input.RotateAxis(forward, StateMgr.transform);
        }


        //--------------
        //移動
        //--------------
        input.AxisL = new Vector2(forward.x, forward.z);

    }

    //固定フレームレートで動く更新
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}

//攻撃状態AIステート
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
