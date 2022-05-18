using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenCharacterController))]
public class CharacterBrain : MonoBehaviour,IDamageApplicable
{
    //アンダーバー表記がメンバー
    //Transform _transform;

    //[Tooltip("[--性能のパラメータ--]")]
    [Header("[--性能のパラメータ--]")]
    [Tooltip("速度")]        [SerializeField] float _moveSpeed    = 1.0f;
    [Tooltip("重力")]        [SerializeField] float _gravity      = -9.8f;
    [Tooltip("減衰")]        [SerializeField] float _attenuation  = 0.85f;
    [Tooltip("ジャンプ力")]  [SerializeField] float _jumpPower    = 4.0f;

    OpenCharacterController _charaCtrl;

    //入力
    InputProvider _inputProvider;

    [Header("[--Component参照--]")]
    [SerializeField] Animator _animator;

    [SerializeField] GameObject _architecture;

    MainObjectParameter _parameter;

    //速度（ベクトルなど）
    Vector3 _velocity;

    private void Awake()
    {
        //_transform = transform;
        _charaCtrl = GetComponent<OpenCharacterController>();

        //自分以下の子のInputProviderを継承したコンポーネントを取得
        _inputProvider = GetComponentInChildren<InputProvider>();

        //parameterを取得
        _parameter = GetComponent<MainObjectParameter>();

    }

    // Update is called once per frame
    void Update()
    {
        //移動(秒速)
        _charaCtrl.Move(_velocity * Time.deltaTime);

        if (_charaCtrl.isGrounded)
        {
            _velocity.y = 0;
        }

    }

    bool IDamageApplicable.ApplyDamage(ref DamageParam param)
    {
        _parameter.hp -= param.DamageValue;
        Debug.Log("Hit"+_parameter.name);
        return true;
    }

    /// <summary>
    /// 立ち状態クラス
    /// </summary>
    [System.Serializable]
    public class ASStand : GameStateMachine.StateNodeBase
    {
        public override void OnExit()
        {
            base.OnExit();
            //Debug.Log("Exit");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            //Debug.Log("Stand");

            //var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            var brain = StateMgr.CharaBrain;

            Vector2 axisL = brain._inputProvider.GetAxisL();

            if (axisL.magnitude > 0.1f)
            {
                brain._animator.SetBool("IsMoving", true);
            }
            //重力
            brain._velocity.y += brain._gravity * Time.deltaTime;

            //攻撃
            if (brain._inputProvider.GetButtonAttack())
            {
                brain._animator.SetTrigger("DoAttack");
            }

            //建築
            if (brain._inputProvider.GetButtonArchitecture())
            {
                Vector3 instantiatePos = brain.transform.position;
                instantiatePos.y = 0.15f;
                Instantiate(brain._architecture,instantiatePos, brain.transform.rotation);
            }

            //ジャンプ
            if (brain._charaCtrl.isGrounded && brain._inputProvider.GetButtonJump())
            {
                brain._animator.SetBool("IsJump", true);

                brain._velocity.y += brain._jumpPower;
            }
        }
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //減衰
            if (brain._charaCtrl.isGrounded)
            {
                brain._velocity *= brain._attenuation;
            }

        }
    }

    /// <summary>
    /// 歩き状態クラス
    /// </summary>
    [System.Serializable]
    public class ASWalk : GameStateMachine.StateNodeBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            var brain = StateMgr.CharaBrain;

            //攻撃
            if (brain._inputProvider.GetButtonAttack())
            {
                brain._animator.SetTrigger("DoAttack");
            }

            Vector2 axisL = brain._inputProvider.GetAxisL();
            if (axisL.magnitude < 0.1f)
            {
                brain._animator.SetBool("IsMoving", false);
                return;
            }

            //建築
            if (brain._inputProvider.GetButtonArchitecture())
            {
                Vector3 instantiatePos = brain.transform.position;
                instantiatePos.y = 0.15f;
                Instantiate(brain._architecture,instantiatePos,brain.transform.rotation);
            }

            //ジャンプ
            if (!brain._charaCtrl.isGrounded)
            {
                brain._animator.SetBool("IsJump", true);
            }

            brain._animator.SetFloat("MoveSpeed", axisL.magnitude);

            float axisPower = axisL.magnitude;

            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);

            //--------------
            //向き
            //--------------
            if (axisPower > 0.01f)
            {
                //入力した方向に回転
                Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                //キャラクターの回転にlerpして回転
                brain.transform.rotation = Quaternion.RotateTowards
                    (
                    brain.transform.rotation,   //変化前の回転
                    rotation,                   //変化後の回転
                    720 * Time.deltaTime        //変化する角度
                    );
            }

            //--------------
            //移動
            //--------------

            if (brain._charaCtrl.isGrounded)
            {
                //速度設定
                forward *= axisPower * brain._moveSpeed;

                //移動方向をリアル時間に直してメインの移動に代入
                brain._velocity += forward * Time.deltaTime;
            }

            //重力
            brain._velocity.y += brain._gravity * Time.deltaTime;

        }

        //固定フレームレートで動く更新
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //減衰
            if (brain._charaCtrl.isGrounded)
            {
                brain._velocity *= brain._attenuation;
            }
        }
    }

    /// <summary>
    /// 攻撃状態クラス
    /// </summary>
    [System.Serializable]
    public class ASAttack : GameStateMachine.StateNodeBase
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

    /// <summary>
    /// ジャンプ状態クラス
    /// </summary>
    [System.Serializable]
    public class ASJump : GameStateMachine.StateNodeBase
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

            //Debug.Log("ジャンプ");

            var brain = StateMgr.CharaBrain;

            //重力
            brain._velocity.y += brain._gravity * Time.deltaTime;

            if (brain._charaCtrl.isGrounded)
            {
                brain._animator.SetBool("IsJump", false);
            }
        }
    }
}
