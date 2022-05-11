using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenCharacterController))]
public class CharacterBrain : MonoBehaviour
{
    //�A���_�[�o�[�\�L�������o�[
    //Transform _transform;

    //[Tooltip("[--���\�̃p�����[�^--]")]
    [Header("[--���\�̃p�����[�^--]")]
    [SerializeField] float _moveSpeed = 1.0f;

    OpenCharacterController _charaCtrl;

    //
    InputProvider _inputProvider;

    [Header("[--Component�Q��--]")]
    [SerializeField] Animator _animator;

    //���x�i�x�N�g���Ȃǁj
    Vector3 _velocity;

    private void Awake()
    {
        //_transform = transform;
        _charaCtrl = GetComponent<OpenCharacterController>();

        //�����ȉ��̎q��InputProvider���p�������R���|�[�l���g���擾
        _inputProvider = GetComponentInChildren<InputProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�(�b��)
        _charaCtrl.Move(_velocity * Time.deltaTime);

        if (_charaCtrl.isGrounded)
        {
            _velocity.y = 0;
        }
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
            //�d��
            brain._velocity.y += -9.8f * Time.deltaTime;

            //�U��
            if (brain._inputProvider.GetButtonAttack())
            {
                brain._animator.SetTrigger("DoAttack");
            }
        }
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //�d��
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
    /// ������ԃN���X
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
            //����
            //--------------
            if (axisPower > 0.01f)
            {
                Vector3 vLook = new Vector3(axisL.x, 0, axisL.y);
                Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);

                brain.transform.rotation = Quaternion.RotateTowards
                    (
                    brain.transform.rotation,   //�ω��O�̉�]
                    rotation,                   //�ω���̉�]
                    720 * Time.deltaTime        //�ω�����p�x
                    );
            }

            //--------------
            //�ړ�
            //--------------

            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
            forward.Normalize();

            forward *= axisPower * brain._moveSpeed;

            brain._velocity += forward * Time.deltaTime;

            //�d��
            brain._velocity.y += -9.8f * Time.deltaTime;

        }

        //�Œ�t���[�����[�g�œ����X�V
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.GetComponentInParent<CharacterBrain>();

            //�d��
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
