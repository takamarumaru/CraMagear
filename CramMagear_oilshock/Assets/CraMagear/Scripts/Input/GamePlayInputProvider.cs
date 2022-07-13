using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GamePlayInputProvider : InputProvider
{
    [Header("[--Cinemachine�Q��--]")]
    [SerializeField] public CinemachineVirtualCamera _virtualCamera;
    [SerializeField] public CinemachineVirtualCamera _aimCamera;
    [SerializeField] GameObject _cameraLookPoint;

    [Header("[--�J�����̏㉺����--]")]
    [SerializeField] float _MaxCamRotateY = 0;
    [SerializeField] float _minCamRotateY = 0;

    [Header("[--���e�B�N���̃A�N�e�B�u��A�N�e�B�u����--]")]
    [SerializeField] GameObject _reticleObject;

    [Header("[--���z--]")]
    [SerializeField] public ArchitectureCreator _architectureCreator;

    private void Awake()
    {
        Debug.Assert(_virtualCamera != null, "GamePlayInputProvider��CinemachineVirtualCamera���ݒ肳��Ă��܂���B");
        Debug.Assert(_aimCamera != null, "GamePlayInputProvider��AimCamera���ݒ肳��Ă��܂���B");
        Debug.Assert(_reticleObject != null, "GamePlayInputProvider��ReticleObject���ݒ肳��Ă��܂���B");
        Debug.Assert(_architectureCreator != null, "GamePlayInputProvider��ArchitectureCreator���ݒ肳��Ă��܂���B");
    }

    //AI����Z�b�g�����f�[�^
    public Vector2 AxisL { get; set; }
    public Vector2 AxisR { get; set; }
    public bool Attack { get; set; } = false;

    //�����擾
    public override Vector2 GetAxisL() => AxisL;
    //�U����Ԃ��ǂ���
    public override bool GetAttackState() => Attack;

    public Vector2 PlayerMoveCameraDirection()
    {
        Vector2 axisL = PlayerInputManager.Instance.GamePlay_GetAxisL();
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        forward = cameraForward * forward.z + Camera.main.transform.right * forward.x;
        forward.Normalize();
        return new Vector2(forward.x, forward.z);
    }

    //�E���擾
    public override Vector2 GetAxisR() => Vector2.zero;

    public override Vector2 GetMouse()
    {
        return PlayerInputManager.Instance.GamePlay_GetMouse();
    }

    //�U���{�^��
    public override bool GetButtonAttack()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAttack();
    }

    //Aim�؂�ւ��{�^��
    public override bool GetButtonAim()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonAim();
    }

    //������
    public override bool GetButtonPressedAim()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonPressedAim();
    }

    public override bool GetButtonArchitectureToggle()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonArchitectureToggle();
    }

    //�W�����v�{�^��
    public override bool GetButtonJump()
    {
        return PlayerInputManager.Instance.GamePlay_GetButtonJump();
    }

    public void PlayerRotation(Vector3 forward, float axisPower, Transform playerTrans)
    {

        //Virtual�J�������Aim�J�����̕����D��x���Ⴉ������J���������Ɍ���
        if (_aimCamera.Priority < _virtualCamera.Priority)
        {
            if (axisPower > 0.01f)
            {
                //���͂��������ɉ�]
                Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

                //�L�����N�^�[�̉�]��lerp���ĉ�]
                playerTrans.rotation = Quaternion.RotateTowards
                     (
                     transform.rotation,         //�ω��O�̉�]
                     rotation,                   //�ω���̉�]
                     720 * Time.deltaTime        //�ω�����p�x
                     );
            }
        }
        else
        {

            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            //���͂��������ɉ�]
            Quaternion rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
            //�L�����N�^�[�̉�]��lerp���ĉ�]
            playerTrans.rotation = rotation;

            //�v���C���[�̉�]
            playerTrans.Rotate(0.0f, GetMouse().x, 0.0f);

            //LookPoint�̉�]
            _cameraLookPoint.transform.Rotate(-GetMouse().y, 0.0f, 0.0f);

            var localAngle = _cameraLookPoint.transform.localEulerAngles;

            //�J��������
            if (_MaxCamRotateY < localAngle.x && localAngle.x < 180)
                localAngle.x = _MaxCamRotateY;
            if (_minCamRotateY + 360 > localAngle.x && localAngle.x > 180)
                localAngle.x = _minCamRotateY + 360;

            //�l��������
            _cameraLookPoint.transform.localEulerAngles = localAngle;

        }

    }


    //�J�����؂�ւ�
    public void SelectUseCamera()
    {
        //�J�����̗D�揇��
        int _EnablePriority = 10;
        int _DisablePriority = 9;

        //�E�N���b�N�i�J�����؂�ւ��j
        if (GetButtonPressedAim())
        {
            _virtualCamera.Priority = _DisablePriority;
            _aimCamera.Priority = _EnablePriority;

            _reticleObject.SetActive(true);
        }
        else
        {
            _virtualCamera.Priority = _EnablePriority;
            _aimCamera.Priority = _DisablePriority;
            _reticleObject.SetActive(false);
        }

    }
}
