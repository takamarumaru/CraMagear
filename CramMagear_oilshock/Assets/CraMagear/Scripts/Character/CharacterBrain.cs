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

    OpenCharacterController _charaCtrl;

    //����
    InputProvider _inputProvider;

    [Header("[--Component�Q��--]")]
    [SerializeField] Animator _animator;

    [SerializeField] ArchitectureCreator _architectureCreator;

    [Header("[--Cinemachine�Q��--]")]
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    [SerializeField] CinemachineVirtualCamera _aimCamera;
    [SerializeField] GameObject _cameraLookPoint;

    [Header("[--�J�����̏㉺����--]")]
    [SerializeField] float _MaxCamRotateY = 0;
    [SerializeField] float _minCamRotateY = 0;
    float _camRotateY = 0;

    MainObjectParameter _parameter;
    public MainObjectParameter MainObjectParam => _parameter;

    //���x�i�x�N�g���Ȃǁj
    Vector3 _velocity;

    private void Awake()
    {
        //_transform = transform;
        _charaCtrl = GetComponent<OpenCharacterController>();

        //�����ȉ��̎q��InputProvider���p�������R���|�[�l���g���擾
        _inputProvider = GetComponentInChildren<InputProvider>();

        //parameter���擾
        _parameter = GetComponent<MainObjectParameter>();

    }

    // Update is called once per frame
    void Update()
    {
        SelectUseCamera();

        //�ړ�(�b��)
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

    //v�����ړ�����
    public void Move(Vector3 v)
    {
        _charaCtrl.Move(v);
    }

    //�v���C���[�̌���
    void PlayerRotation(Vector3 forward, float axisPower)
    {

        //Virtual�J�������Aim�J�����̕����D��x���Ⴉ������J���������Ɍ���
        if (_aimCamera.Priority < _virtualCamera.Priority)
        {
            if (axisPower > 0.01f)
            {
                //���͂��������ɉ�]
                Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                //�L�����N�^�[�̉�]��lerp���ĉ�]
                transform.rotation = Quaternion.RotateTowards
                     (
                     transform.rotation,   //�ω��O�̉�]
                     rotation,                   //�ω���̉�]
                     720 * Time.deltaTime        //�ω�����p�x
                     );
            }
        }
        else
        {
            //�J�����̌����Ă�����Ƀv���C���[�������i�����ꂽ�������j
            if(_inputProvider.GetButtonAim())
            {
                // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
                Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                //���͂��������ɉ�]
                Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                //�L�����N�^�[�̉�]��lerp���ĉ�]
                transform.rotation = rotation;
            }

            //�v���C���[�̉�]
            transform.Rotate(0.0f, _inputProvider.GetMouse().x, 0.0f);

            //LookPoint�̉�]
            _cameraLookPoint.transform.Rotate(-_inputProvider.GetMouse().y, 0.0f, 0.0f);

            var localAngle = _cameraLookPoint.transform.localEulerAngles;

            //�J��������
            if (_MaxCamRotateY < localAngle.x && localAngle.x < 180)
                localAngle.x = _MaxCamRotateY;
            if (_minCamRotateY > localAngle.x && localAngle.x > 180)
                localAngle.x = _minCamRotateY;

            //�l��������
            _cameraLookPoint.transform.localEulerAngles = localAngle;

        }

    }

    //�J�����؂�ւ�
    void SelectUseCamera()
    {
        if (_parameter.Name != "Player") { return; }

        //�J�����̗D�揇��
        int _EnablePriority = 10;
        int _DisablePriority = 9;

        //�E�N���b�N�i�J�����؂�ւ��j
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
            //�d��
            //brain._velocity.y += brain._gravity * Time.deltaTime;

            //���z�؂�ւ�
            if (brain._inputProvider.GetButtonArchitectureToggle())
            {
                brain._architectureCreator.EnableToggle();
            }

            //�U��
            if (brain._inputProvider.GetButtonAttack())
            {
                //���z���삪�L���Ȃ猚�z
                if (brain._architectureCreator && brain._architectureCreator._enable == true)
                {
                    brain._architectureCreator.Create();
                }
                else
                //���z���삪�����Ȃ�U��
                {
                    brain._animator.SetTrigger("DoAttack");

                    if (brain._parameter.Name == "Player")
                    {
                        //Virtual�J�������Aim�J�����̕����D��x���Ⴉ������J���������Ɍ���
                        if (brain._aimCamera.Priority < brain._virtualCamera.Priority)
                        {
                            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
                            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                            //���͂��������ɉ�]
                            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                            //�L�����N�^�[�̉�]��lerp���ĉ�]
                            brain.transform.rotation = rotation;
                        }
                    }

                }
            }

            if (brain._parameter.Name == "Player")
            {
                //�v���C���[�̌���
                brain.PlayerRotation(new Vector3(0, 0, 0), 0.0f);
            }
            else
            {
                float axisPower = axisL.magnitude;

                Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
                if (axisPower > 0.01f)
                {
                    //���͂��������ɉ�]
                    Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                    //�L�����N�^�[�̉�]��lerp���ĉ�]
                    brain.transform.rotation = Quaternion.RotateTowards
                         (
                         brain.transform.rotation,   //�ω��O�̉�]
                         rotation,                   //�ω���̉�]
                         720 * Time.deltaTime        //�ω�����p�x
                         );
                }
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

            //���z�؂�ւ�
            if (brain._inputProvider.GetButtonArchitectureToggle())
            {
                brain._architectureCreator.EnableToggle();
            }

            //����{�^��
            if (brain._inputProvider.GetButtonAttack())
            {
                //���z���삪�L���Ȃ猚�z
                if (brain._architectureCreator && brain._architectureCreator._enable == true)
                {
                    brain._architectureCreator.Create();
                }
                else
                //���z���삪�����Ȃ�U��
                {
                    brain._animator.SetTrigger("DoAttack");

                    if (brain._parameter.Name == "Player")
                    {
                        //Virtual�J�������Aim�J�����̕����D��x���Ⴉ������J���������Ɍ���
                        if (brain._aimCamera.Priority < brain._virtualCamera.Priority)
                        {
                            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
                            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                            //���͂��������ɉ�]
                            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                            //�L�����N�^�[�̉�]��lerp���ĉ�]
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

            //--------------
            //����
            //--------------
            if (brain._parameter.Name == "Player")
            {
                //�v���C���[�̌���
                brain.PlayerRotation(forward, axisPower);
            }
            else
            {
                if (axisPower > 0.01f)
                {
                    //���͂��������ɉ�]
                    Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                    //�L�����N�^�[�̉�]��lerp���ĉ�]
                    brain.transform.rotation = Quaternion.RotateTowards
                         (
                         brain.transform.rotation,   //�ω��O�̉�]
                         rotation,                   //�ω���̉�]
                         720 * Time.deltaTime        //�ω�����p�x
                         );
                }
            }

            if(brain.transform.name == "PreEnemy")
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
