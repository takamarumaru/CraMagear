using StandardAssets.Characters.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenCharacterController))]
public class CharacterBrain : MonoBehaviour
{

    [Header("[--性能のパラメータ--]")]
    [SerializeField] float _moveSpeed = 1.0f;

    //キャラクターコントローラー
    OpenCharacterController _charCtrl;

    //InputProvider
    InputProvider _inputProvider;

    BulletShot _bulletShot;

    UnityEngine.InputSystem.PlayerInput _input;

    //速度（ベクトルなど）
    Vector3 _velocity;

    private void Awake()
    {
        _charCtrl = GetComponent<OpenCharacterController>();
        TryGetComponent(out _input);

        //自分以下の子のInputProviderを継承したコンポーネントを取得
        _inputProvider = GetComponentInChildren<InputProvider>();

        //銃のコンポーネント取得
        _bulletShot = GetComponentInChildren<BulletShot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputProvider.GetButtonAttack())
        {
            //弾生成、発射
            _bulletShot.LauncherShot();
        }

        //入力情報
        var axisL = _inputProvider.GetAxisL();

        float axisPower = axisL.magnitude;
        //--------------
        //向き
        //--------------
        if (axisPower > 0.01f)
        {
            Vector3 vLook = new Vector3(axisL.x, 0, axisL.y);
            Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);

            transform.rotation = Quaternion.RotateTowards
                (
                transform.rotation,   //変化前の回転
                rotation,                   //変化後の回転
                720 * Time.deltaTime        //変化する角度
                );
        }

        //-----------
        //移動
        //-----------

        Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
        forward.Normalize();
        forward *= axisPower * _moveSpeed;
        _velocity = forward * Time.deltaTime;
        //重力
        _velocity.y += -9.8f * Time.deltaTime;
        if (_charCtrl.isGrounded)
        {
            _velocity *= 0.85f;
        }
        else
        {
            _velocity *= 0.99f;
        }

        //移動(秒速)
        _charCtrl.Move(_velocity * Time.deltaTime);

        if (_charCtrl.isGrounded)
        {
            _velocity.y = 0;
        }
    }
}
