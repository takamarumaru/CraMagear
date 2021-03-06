using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel状態AIステート
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

        //攻撃
        if (input.GetButtonAttack())
        {
            Animator.SetTrigger("DoAttack");
        }

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        if (axisPower > 0.01f)
        {
            input.RotateAxis(forward, StateMgr.CharaBrain.transform);
        }

        //ジャンプ
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

//追跡状態AIステート
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

        //決定ボタン
        if (input.GetButtonAttack())
        {

            Animator.SetTrigger("DoAttack");

            //// カメラの方向から、X-Z平面の単位ベクトルを取得
            //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            ////入力した方向に回転
            //Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
            ////キャラクターの回転にlerpして回転
            //StateMgr.CharaBrain.transform.rotation = rotation;
        }

        Vector2 axisL = input.GetAxisL();
        if (axisL.magnitude < 0.1f)
        {
            Animator.SetBool("IsMoving", false);
            return;
        }

        //ジャンプ
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
