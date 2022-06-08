using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel状態ステート
[System.Serializable]
public class PlayerStateIdle : GameStateMachine.StateNodeBase
{
    public override void OnExit()
    {
        base.OnExit();
        //Debug.Log("Exit");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<GamePlayInputProvider>();

        //建築半透明オブジェクト処理
        if (input._architectureCreator)
        {
            input._architectureCreator.ShowGuide();
        }

        input.SelectUseCamera();

        //カメラ方向に移動
        Vector2 axisL = input.PlayerMoveCameraDirection();

        if (axisL.magnitude > 0.1f)
        {
            Animator.SetBool("IsMoving", true);
        }

        //建築切り替え
        if (input.GetButtonArchitectureToggle())
        {
            input._architectureCreator.EnableToggle();
        }

        //攻撃
        if (input.GetButtonAttack())
        {
            //建築操作が有効なら建築
            if (input._architectureCreator && input._architectureCreator._enable == true)
            {
                input._architectureCreator.Create();
            }
            else
            //建築操作が無効なら攻撃
            {
                Animator.SetTrigger("DoAttack");

                //VirtualカメラよりAimカメラの方が優先度が低かったらカメラ方向に撃つ
                if (input._aimCamera.Priority < input._virtualCamera.Priority)
                {
                    // カメラの方向から、X-Z平面の単位ベクトルを取得
                    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    //入力した方向に回転
                    Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                    //キャラクターの回転にlerpして回転
                    StateMgr.CharaBrain.transform.rotation = rotation;
                }

            }
        }

        //プレイヤーの向き
        input.PlayerRotation(new Vector3(0, 0, 0), 0.0f, StateMgr.CharaBrain.transform);

        input.AxisL = axisL;

        //ジャンプ
        if (input.GetButtonJump() && StateMgr.CharaBrain._charaCtrl.isGrounded)
        {
            Animator.SetBool("IsJump", true);
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}

//歩き状態ステート
[System.Serializable]
public class PlayerStateWalk : GameStateMachine.StateNodeBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = StateMgr.GetComponent<GamePlayInputProvider>();

        //建築半透明オブジェクト処理
        if (input._architectureCreator)
        {
            input._architectureCreator.ShowGuide();
        }

        input.SelectUseCamera();

        //建築切り替え
        if (input.GetButtonArchitectureToggle())
        {
            input._architectureCreator.EnableToggle();
        }

        //決定ボタン
        if (input.GetButtonAttack())
        {
            //建築操作が有効なら建築
            if (input._architectureCreator && input._architectureCreator._enable == true)
            {
                input._architectureCreator.Create();
            }
            else
            //建築操作が無効なら攻撃
            {
                Animator.SetTrigger("DoAttack");

                //VirtualカメラよりAimカメラの方が優先度が低かったらカメラ方向に撃つ
                if (input._aimCamera.Priority < input._virtualCamera.Priority)
                {
                    // カメラの方向から、X-Z平面の単位ベクトルを取得
                    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    //入力した方向に回転
                    Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                    //キャラクターの回転にlerpして回転
                    StateMgr.CharaBrain.transform.rotation = rotation;

                    return;
                }
            }
        }

        Vector2 axisL = input.PlayerMoveCameraDirection();
        if (axisL.magnitude < 0.1f)
        {
            Animator.SetBool("IsMoving", false);
            return;
        }

        //ジャンプ
        if (input.GetButtonJump() && StateMgr.CharaBrain._charaCtrl.isGrounded)
        {
            Animator.SetBool("IsJump", true);
            return;
        }

        Animator.SetFloat("MoveSpeed", axisL.magnitude);

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);

        //--------------
        //向き
        //--------------
        //プレイヤーの向き
        input.PlayerRotation(forward, axisPower, StateMgr.CharaBrain.transform);

        input.AxisL = new Vector2(forward.x, forward.z);

    }

    //固定フレームレートで動く更新
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}

//攻撃状態ステート
[System.Serializable]
public class PlayerStateAttack : GameStateMachine.StateNodeBase
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

        var input = StateMgr.GetComponent<GamePlayInputProvider>();

        input.SelectUseCamera();
    }
}

//ジャンプ状態ステート
[System.Serializable]
public class PlayerStateJump : GameStateMachine.StateNodeBase
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

        var input = StateMgr.GetComponent<GamePlayInputProvider>();

        input.SelectUseCamera();

        Animator.SetBool("IsJump", false);

    }
}
