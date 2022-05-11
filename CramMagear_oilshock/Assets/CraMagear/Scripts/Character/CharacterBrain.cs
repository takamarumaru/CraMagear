using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenCharacterController))]
public class CharacterBrain : MonoBehaviour
{
    //アンダーバー表記がメンバー
    //Transform _transform;

    //[Tooltip("[--性能のパラメータ--]")]
    [Header("[--性能のパラメータ--]")]
    [SerializeField] float _moveSpeed = 1.0f;

    OpenCharacterController _charaCtrl;

    //
    InputProvider _inputProvider;

    [Header("[--Component参照--]")]
    [SerializeField] Animator _animator;

    //速度（ベクトルなど）
    Vector3 _velocity;

    private void Awake()
    {
        //_transform = transform;
        _charaCtrl = GetComponent<OpenCharacterController>();

        //自分以下の子のInputProviderを継承したコンポーネントを取得
        _inputProvider = GetComponentInChildren<InputProvider>();
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

    /// <summary>
    /// 立ち状態クラス
    /// </summary>
    [System.Serializable]
    public class ASStand : GameStateMachine.StateNodeBase
    {
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exit");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.Log("Stand");

            //var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            var brain = StateMgr.CharaBrain;

            Vector2 axisL = brain._inputProvider.GetAxisL();

            if (axisL.magnitude > 0.1f)
            {
                brain._animator.SetBool("IsMoving", true);
            }
            //重力
            brain._velocity.y += -9.8f * Time.deltaTime;

            //攻撃
            if (brain._inputProvider.GetButtonAttack())
            {
                brain._animator.SetTrigger("DoAttack");
            }
        }
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //重力
            if (brain._charaCtrl.isGrounded)
            {
                brain._velocity *= 0.85f;
            }
            else
            {
                brain._velocity *= 0.98f;
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
            Debug.Log("Enter");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.Log("Walk");

            //var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            var brain = StateMgr.CharaBrain;

            Vector2 axisL = brain._inputProvider.GetAxisL();
            if (axisL.magnitude < 0.1f)
            {
                brain._animator.SetBool("IsMoving", false);
            }

            brain._animator.SetFloat("MoveSpeed", axisL.magnitude);

            float axisPower = axisL.magnitude;
            //--------------
            //向き
            //--------------
            if (axisPower > 0.01f)
            {
                Vector3 vLook = new Vector3(axisL.x, 0, axisL.y);
                Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);

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

            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
            forward.Normalize();

            forward *= axisPower * brain._moveSpeed;

            brain._velocity += forward * Time.deltaTime;

            //重力
            brain._velocity.y += -9.8f * Time.deltaTime;

        }

        //固定フレームレートで動く更新
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //重力
            if (brain._charaCtrl.isGrounded)
            {
                brain._velocity *= 0.85f;
            }
            else
            {
                brain._velocity *= 0.98f;
            }
        }
    }

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
}
