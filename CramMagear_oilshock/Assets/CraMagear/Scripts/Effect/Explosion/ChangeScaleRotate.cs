using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleRotate : MonoBehaviour
{
    [SerializeField] float _MaxScale = 0.0f;
    [SerializeField] float _rotateSpeed = 0.0f;

    [Header("�����͈̓Z�b�g")]
    [SerializeField] GameObject _explosionRange;

    //�X�P�[���̒l
    float _scaleValue = 0.1f;

    //�o���ꏊ�ۑ��p
    public Vector3 _popPosition { get; set; }

    //AddComponent�����Ƃ��Ɏg���p�i���@�w�̎��j--------------------------
    public float MaxScale => _MaxScale;
    public float RotateSpeed => _rotateSpeed;

    public GameObject ExplosionRangeObj => _explosionRange;

    public void SetCopyMaxScaleAndRotateSpeed(float scale, float rot)
    {
        _MaxScale = scale;
        _rotateSpeed = rot;
    }

    public void SetCopyGameObject(GameObject obj)
    {
        _explosionRange = obj;
    }

    //-------------------------------------------------------------------

    private void Awake()
    {
    }

    private void Update()
    {
        //�o���ꏊ�ɏo������
        transform.position = _popPosition;

        //�g�又��
        if (_MaxScale < _scaleValue)
        {
            _scaleValue = _MaxScale;

            Debug.Assert(_explosionRange != null, "Explosion��ExplosionRange���ݒ肳��Ă��܂���B");

            //�����͈͍쐬
            Instantiate(_explosionRange, transform.position, transform.rotation);

            //�L����؂����疂�@�w��j��
            Destroy(gameObject);

        }
        else
        {
            //���̃T�C�Y�܂ł����Ă��Ȃ��������]���Ȃ���g��
            transform.localScale = new Vector3(_scaleValue, _scaleValue, _scaleValue);
            transform.Rotate(0, _rotateSpeed, 0);
        }

        _scaleValue += 0.5f + Time.deltaTime;


    }
}
