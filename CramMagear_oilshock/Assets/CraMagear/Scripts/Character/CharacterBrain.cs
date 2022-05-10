using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenCharacterController))]
public class CharacterBrain : MonoBehaviour
{

    [Header("[--���\�̃p�����[�^--]")]
    [SerializeField] float _moveSpeed = 1.0f;

    //�L�����N�^�[�R���g���[���[
    OpenCharacterController _charCtrl;

    //InputProvider
    InputProvider _inputProvider;

    BulletShot _bulletShot;

    UnityEngine.InputSystem.PlayerInput _input;

    //���x�i�x�N�g���Ȃǁj
    Vector3 _velocity;

    private void Awake()
    {
        _charCtrl = GetComponent<OpenCharacterController>();
        TryGetComponent(out _input);

        //�����ȉ��̎q��InputProvider���p�������R���|�[�l���g���擾
        _inputProvider = GetComponentInChildren<InputProvider>();

        //�e�̃R���|�[�l���g�擾
        _bulletShot = GetComponentInChildren<BulletShot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputProvider.GetButtonAttack())
        {
            //�e�����A����
            _bulletShot.LauncherShot();
        }

        //���͏��
        var axisL = _inputProvider.GetAxisL();

        float axisPower = axisL.magnitude;
        //--------------
        //����
        //--------------
        if (axisPower > 0.01f)
        {
            Vector3 vLook = new Vector3(axisL.x, 0, axisL.y);
            Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);

            transform.rotation = Quaternion.RotateTowards
                (
                transform.rotation,   //�ω��O�̉�]
                rotation,                   //�ω���̉�]
                720 * Time.deltaTime        //�ω�����p�x
                );
        }

        //-----------
        //�ړ�
        //-----------

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        forward.Normalize();
        forward *= axisPower * _moveSpeed;
        _velocity = forward * Time.deltaTime;
        //�d��
        _velocity.y += -9.8f * Time.deltaTime;
        if (_charCtrl.isGrounded)
        {
            _velocity *= 0.85f;
        }
        else
        {
            _velocity *= 0.99f;
        }

        //�ړ�(�b��)
        _charCtrl.Move(_velocity * Time.deltaTime);

        if (_charCtrl.isGrounded)
        {
            _velocity.y = 0;
        }
    }
}
