using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(OpenCharacterController))]
[RequireComponent(typeof(MainObjectParameter))]
public class CharacterBrain : MonoBehaviour, IDamageApplicable
{
    //アンダーバー表記がメンバー
    //Transform _transform;

    //[Tooltip("[--性能のパラメータ--]")]
    [Header("[--性能のパラメータ--]")]
    [Tooltip("速度")][SerializeField] float _moveSpeed = 1.0f;
    [Tooltip("重力")][SerializeField] float _gravity = -9.8f;
    [Tooltip("減衰")][SerializeField] float _attenuation = 0.85f;
    [Tooltip("ジャンプ力")][SerializeField] float _jumpPower = 4.0f;

    OpenCharacterController _charaCtrl;

    //入力
    InputProvider _inputProvider;

    [Header("[--Component参照--]")]
    [SerializeField] Animator _animator;

    [SerializeField] ArchitectureCreator _architectureCreator;

    [Header("[--Cinemachine参照--]")]
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    [SerializeField] CinemachineVirtualCamera _aimCamera;
    [SerializeField] GameObject _cameraLookPoint;

    [Header("[--カメラの上下制御--]")]
    [SerializeField] float _MaxCamRotateY = 0;
    [SerializeField] float _minCamRotateY = 0;
    float _camRotateY = 0;

    MainObjectParameter _parameter;
    public MainObjectParameter MainObjectParam => _parameter;

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
        SelectUseCamera();

        //移動(秒速)
        _charaCtrl.Move(_velocity);

        if (_charaCtrl.isGrounded)
        {
            _velocity.y = 0;
        }

        if (_architectureCreator)
        {
            _architectureCreator.ShowGuide();
        }

    }

    //vだけ移動する
    public void Move(Vector3 v)
    {
        _charaCtrl.Move(v);
    }

    //プレイヤーの向き
    void PlayerRotation(Vector3 forward, float axisPower)
    {

        //VirtualカメラよりAimカメラの方が優先度が低かったらカメラ方向に撃つ
        if (_aimCamera.Priority < _virtualCamera.Priority)
        {
            if (axisPower > 0.01f)
            {
                //入力した方向に回転
                Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                //キャラクターの回転にlerpして回転
                transform.rotation = Quaternion.RotateTowards
                     (
                     transform.rotation,   //変化前の回転
                     rotation,                   //変化後の回転
                     720 * Time.deltaTime        //変化する角度
                     );
            }
        }
        else
        {
            //カメラの向いている方にプレイヤーも向く（押された時だけ）
            if(_inputProvider.GetButtonAim())
            {
                // カメラの方向から、X-Z平面の単位ベクトルを取得
                Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                //入力した方向に回転
                Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                //キャラクターの回転にlerpして回転
                transform.rotation = rotation;
            }

            //プレイヤーの回転
            transform.Rotate(0.0f, _inputProvider.GetMouse().x, 0.0f);

            //LookPointの回転
            _cameraLookPoint.transform.Rotate(-_inputProvider.GetMouse().y, 0.0f, 0.0f);

            var localAngle = _cameraLookPoint.transform.localEulerAngles;

            //カメラ制御
            if (_MaxCamRotateY < localAngle.x && localAngle.x < 180)
                localAngle.x = _MaxCamRotateY;
            if (_minCamRotateY > localAngle.x && localAngle.x > 180)
                localAngle.x = _minCamRotateY;

            //値を代入する
            _cameraLookPoint.transform.localEulerAngles = localAngle;

        }

    }

    //カメラ切り替え
    void SelectUseCamera()
    {
        if (_parameter.Name != "Player") { return; }

        //カメラの優先順位
        int _EnablePriority = 10;
        int _DisablePriority = 9;

        //右クリック（カメラ切り替え）
        if (_inputProvider.GetButtonPressedAim())
        {
            _virtualCamera.Priority = _DisablePriority;
            _aimCamera.Priority = _EnablePriority;
        }
        else
        {
            _virtualCamera.Priority = _EnablePriority;
            _aimCamera.Priority = _DisablePriority;
        }

    }

    bool IDamageApplicable.ApplyDamage(ref DamageParam param)
    {
        _parameter.hp -= param.DamageValue;
        Debug.Log("Hit" + _parameter.name);
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
            //brain._velocity.y += brain._gravity * Time.deltaTime;

            //建築切り替え
            if (brain._inputProvider.GetButtonArchitectureToggle())
            {
                brain._architectureCreator.EnableToggle();
            }

            //攻撃
            if (brain._inputProvider.GetButtonAttack())
            {
                //建築操作が有効なら建築
                if (brain._architectureCreator && brain._architectureCreator._enable == true)
                {
                    brain._architectureCreator.Create();
                }
                else
                //建築操作が無効なら攻撃
                {
                    brain._animator.SetTrigger("DoAttack");

                    if (brain._parameter.Name == "Player")
                    {
                        //VirtualカメラよりAimカメラの方が優先度が低かったらカメラ方向に撃つ
                        if (brain._aimCamera.Priority < brain._virtualCamera.Priority)
                        {
                            // カメラの方向から、X-Z平面の単位ベクトルを取得
                            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                            //入力した方向に回転
                            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                            //キャラクターの回転にlerpして回転
                            brain.transform.rotation = rotation;
                        }
                    }

                }
            }

            if (brain._parameter.Name == "Player")
            {
                //プレイヤーの向き
                brain.PlayerRotation(new Vector3(0, 0, 0), 0.0f);
            }
            else
            {
                float axisPower = axisL.magnitude;

                Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
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
            }

            //ジャンプ
            if (brain._charaCtrl.isGrounded && brain._inputProvider.GetButtonJump())
            {
                brain._animator.SetBool("IsJump", true);

                brain._velocity.y += brain._jumpPower;
            }

            //重力
            brain._velocity.y += brain._gravity * Time.deltaTime;
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

            //建築切り替え
            if (brain._inputProvider.GetButtonArchitectureToggle())
            {
                brain._architectureCreator.EnableToggle();
            }

            //決定ボタン
            if (brain._inputProvider.GetButtonAttack())
            {
                //建築操作が有効なら建築
                if (brain._architectureCreator && brain._architectureCreator._enable == true)
                {
                    brain._architectureCreator.Create();
                }
                else
                //建築操作が無効なら攻撃
                {
                    brain._animator.SetTrigger("DoAttack");

                    if (brain._parameter.Name == "Player")
                    {
                        //VirtualカメラよりAimカメラの方が優先度が低かったらカメラ方向に撃つ
                        if (brain._aimCamera.Priority < brain._virtualCamera.Priority)
                        {
                            // カメラの方向から、X-Z平面の単位ベクトルを取得
                            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                            //入力した方向に回転
                            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                            //キャラクターの回転にlerpして回転
                            brain.transform.rotation = rotation;

                            return;
                        }
                    }
                }
            }

            Vector2 axisL = brain._inputProvider.GetAxisL();
            if (axisL.magnitude < 0.1f)
            {
                brain._animator.SetBool("IsMoving", false);
                return;
            }

            //ジャンプ
            if (brain._charaCtrl.isGrounded && brain._inputProvider.GetButtonJump())
            {
                brain._animator.SetBool("IsJump", true);

                brain._velocity.y += brain._jumpPower;
                return;
            }

            brain._animator.SetFloat("MoveSpeed", axisL.magnitude);

            float axisPower = axisL.magnitude;

            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);

            //--------------
            //向き
            //--------------
            if (brain._parameter.Name == "Player")
            {
                //プレイヤーの向き
                brain.PlayerRotation(forward, axisPower);
            }
            else
            {
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
            }

            if(brain.transform.name == "PreEnemy")
            {
                Debug.Log(forward);
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
