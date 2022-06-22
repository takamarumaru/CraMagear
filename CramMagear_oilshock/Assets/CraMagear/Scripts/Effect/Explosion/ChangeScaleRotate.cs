using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleRotate : MonoBehaviour
{
    [SerializeField] float _MaxScale = 20.0f;
    [SerializeField] float _rotateSpeed = 10.0f;

    //�X�P�[���̒l
    float _scaleValue = 0.1f;

    //�o���ꏊ�ۑ��p
    public Vector3 _popPosition { get; set; }

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
