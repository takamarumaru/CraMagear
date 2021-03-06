using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel状態AIステート
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

        //電撃かどうか
        if (StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", true);
            Debug.Log("スタン");
        }

        //攻撃
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

//追跡状態AIステート
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

        //電撃かどうか
        if (StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", true);
            Debug.Log("スタン");
            return;
        }

        //決定ボタン
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
        //向き
        //--------------
        if (axisPower > 0.01f)
        {
            //入力した方向に回転
            Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

            //変化前の回転
            Quaternion prevRotation = StateMgr.CharaBrain.transform.rotation;

            //キャラクターの回転にlerpして回転
            StateMgr.CharaBrain.transform.rotation = Quaternion.RotateTowards
                         (
                         prevRotation,               //変化前の回転
                         rotation,                   //変化後の回転
                         720 * Time.deltaTime        //変化する角度
                         );
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

        //電撃かどうか
        if (StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", true);
        }
    }
}

//電撃状態AIステート
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

        //電撃かどうか
        if (!StateMgr.CharaBrain.CharacterAnimator.GetBool("IsElectricShock"))
        {
            Animator.SetBool("IsElectricShock", false);
        }
    }
}
