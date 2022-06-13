using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(OpenCharacterController))]
[RequireComponent(typeof(MainObjectParameter))]
public class CharacterBrain : MonoBehaviour, IDamageApplicable
{
    //�A���_�[�o�[�\�L�������o�[
    //Transform _transform;

    //[Tooltip("[--���\�̃p�����[�^--]")]
    [Header("[--���\�̃p�����[�^--]")]
    [Tooltip("���x")][SerializeField] float _moveSpeed = 1.0f;
    [Tooltip("�d��")][SerializeField] float _gravity = -9.8f;
    [Tooltip("����")][SerializeField] float _attenuation = 0.85f;
    [Tooltip("�W�����v��")][SerializeField] float _jumpPower = 4.0f;

    public OpenCharacterController _charaCtrl;

    //����
    InputProvider _inputProvider;

    [Header("[--Component�Q��--]")]
    [SerializeField] Animator _animator;

    MainObjectParameter _parameter;
    public MainObjectParameter MainObjectParam => _parameter;

    //���z�̏����擾���邽��
    ArchitectureCreator _architectureCreator;

    //���x�i�x�N�g���Ȃǁj
    Vector3 _velocity;

    private void Awake()
    {
        Debug.Assert(_animator != null, "CharacterBrain�ɃA�j���[�^�[���ݒ肳��Ă��܂���B");

        //_transform = transform;
        _charaCtrl = GetComponent<OpenCharacterController>();

        //�����ȉ��̎q��InputProvider���p�������R���|�[�l���g���擾
        _inputProvider = GetComponentInChildren<InputProvider>();

        //parameter���擾
        _parameter = GetComponent<MainObjectParameter>();

        //���z�擾
        _architectureCreator = GetComponentInChildren<ArchitectureCreator>();

    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�(�b��)
        _charaCtrl.Move(_velocity);

        if (_charaCtrl.isGrounded)
        {
            _velocity.y = 0;
        }

        //�|���A�j���[�V����
        if (_parameter.hp <= 0)
        {
            _animator.SetBool("IsDown", true);
        }

    }

    //v�����ړ�����
    public void Move(Vector3 v)
    {
        //_charaCtrl.Move(v);
    }

    bool IDamageApplicable.ApplyDamage(ref DamageParam param)
    {
        //�U���H������A�j���[�V����
        if (_parameter.Name == "Player")
        {
            _animator.SetTrigger("DoStagger");
        }

        _parameter.hp -= param.DamageValue;
        Debug.Log("Hit" + _parameter.name);
        return true;
    }

    /// <summary>
    /// ������ԃN���X
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

            //�U��
            if (brain._inputProvider.GetButtonAttack())
            {
                brain._animator.SetTrigger("DoAttack");

                return;
            }

            //�W�����v
            if (brain._charaCtrl.isGrounded && brain._inputProvider.GetButtonJump())
            {
                brain._animator.SetBool("IsJump", true);

                brain._velocity.y += brain._jumpPower;
            }

            //�d��
            brain._velocity.y += brain._gravity * Time.deltaTime;
        }
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //����
            if (brain._charaCtrl.isGrounded)
            {
                brain._velocity *= brain._attenuation;
            }

        }
    }

    /// <summary>
    /// ������ԃN���X
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

            //����{�^��
            if (brain._inputProvider.GetButtonAttack())
            {

                brain._animator.SetTrigger("DoAttack");

                return;
            }

            Vector2 axisL = brain._inputProvider.GetAxisL();
            if (axisL.magnitude < 0.1f)
            {
                brain._animator.SetBool("IsMoving", false);
                return;
            }

            //�W�����v
            if (brain._charaCtrl.isGrounded && brain._inputProvider.GetButtonJump())
            {
                brain._animator.SetBool("IsJump", true);

                brain._velocity.y += brain._jumpPower;
                return;
            }

            brain._animator.SetFloat("MoveSpeed", axisL.magnitude);

            float axisPower = axisL.magnitude;

            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);

            if (brain.transform.name == "PreEnemy")
            {
                Debug.Log(forward);
            }

            //--------------
            //�ړ�
            //--------------

            if (brain._charaCtrl.isGrounded)
            {
                //���x�ݒ�
                forward *= axisPower * brain._moveSpeed;

                //�ړ����������A�����Ԃɒ����ă��C���̈ړ��ɑ��
                brain._velocity += forward * Time.deltaTime;
            }

            //�d��
            brain._velocity.y += brain._gravity * Time.deltaTime;

        }

        //�Œ�t���[�����[�g�œ����X�V
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //����
            if (brain._charaCtrl.isGrounded)
            {
                brain._velocity *= brain._attenuation;
            }
        }
    }

    /// <summary>
    /// �U����ԃN���X
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
    /// �W�����v��ԃN���X
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

            //Debug.Log("�W�����v");

            var brain = StateMgr.CharaBrain;

            //�d��
            brain._velocity.y += brain._gravity * Time.deltaTime;

            if (brain._charaCtrl.isGrounded)
            {
                brain._animator.SetBool("IsJump", false);
            }
        }
    }
}
