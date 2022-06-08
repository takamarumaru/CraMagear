using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idel��ԃX�e�[�g
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

        //���z�������I�u�W�F�N�g����
        if (input._architectureCreator)
        {
            input._architectureCreator.ShowGuide();
        }

        input.SelectUseCamera();

        //�J���������Ɉړ�
        Vector2 axisL = input.PlayerMoveCameraDirection();

        if (axisL.magnitude > 0.1f)
        {
            Animator.SetBool("IsMoving", true);
        }

        //���z�؂�ւ�
        if (input.GetButtonArchitectureToggle())
        {
            input._architectureCreator.EnableToggle();
        }

        //�U��
        if (input.GetButtonAttack())
        {
            //���z���삪�L���Ȃ猚�z
            if (input._architectureCreator && input._architectureCreator._enable == true)
            {
                input._architectureCreator.Create();
            }
            else
            //���z���삪�����Ȃ�U��
            {
                Animator.SetTrigger("DoAttack");

                //Virtual�J�������Aim�J�����̕����D��x���Ⴉ������J���������Ɍ���
                if (input._aimCamera.Priority < input._virtualCamera.Priority)
                {
                    // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
                    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    //���͂��������ɉ�]
                    Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                    //�L�����N�^�[�̉�]��lerp���ĉ�]
                    StateMgr.CharaBrain.transform.rotation = rotation;
                }

            }
        }

        //�v���C���[�̌���
        input.PlayerRotation(new Vector3(0, 0, 0), 0.0f, StateMgr.CharaBrain.transform);

        input.AxisL = axisL;

        //�W�����v
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

//������ԃX�e�[�g
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

        //���z�������I�u�W�F�N�g����
        if (input._architectureCreator)
        {
            input._architectureCreator.ShowGuide();
        }

        input.SelectUseCamera();

        //���z�؂�ւ�
        if (input.GetButtonArchitectureToggle())
        {
            input._architectureCreator.EnableToggle();
        }

        //����{�^��
        if (input.GetButtonAttack())
        {
            //���z���삪�L���Ȃ猚�z
            if (input._architectureCreator && input._architectureCreator._enable == true)
            {
                input._architectureCreator.Create();
            }
            else
            //���z���삪�����Ȃ�U��
            {
                Animator.SetTrigger("DoAttack");

                //Virtual�J�������Aim�J�����̕����D��x���Ⴉ������J���������Ɍ���
                if (input._aimCamera.Priority < input._virtualCamera.Priority)
                {
                    // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
                    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    //���͂��������ɉ�]
                    Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                    //�L�����N�^�[�̉�]��lerp���ĉ�]
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

        //�W�����v
        if (input.GetButtonJump() && StateMgr.CharaBrain._charaCtrl.isGrounded)
        {
            Animator.SetBool("IsJump", true);
            return;
        }

        Animator.SetFloat("MoveSpeed", axisL.magnitude);

        float axisPower = axisL.magnitude;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);

        //--------------
        //����
        //--------------
        //�v���C���[�̌���
        input.PlayerRotation(forward, axisPower, StateMgr.CharaBrain.transform);

        input.AxisL = new Vector2(forward.x, forward.z);

    }

    //�Œ�t���[�����[�g�œ����X�V
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}

//�U����ԃX�e�[�g
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

//�W�����v��ԃX�e�[�g
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
