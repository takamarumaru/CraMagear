using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleRotate : MonoBehaviour
{
    [SerializeField] float _MaxScale = 20.0f;
    [SerializeField] float _rotateSpeed = 10.0f;

    //スケールの値
    float _scaleValue = 0.1f;

    //出現場所保存用
    public Vector3 _popPosition { get; set; }

    private void Awake()
    {
       
    }

    private void Update()
    {
        //出現場所に出すため
        transform.position = _popPosition;

        //拡大処理
        if (_MaxScale < _scaleValue)
        {
            _scaleValue = _MaxScale;
        }
        else
        {
            //一定のサイズまでいっていなかったら回転しながら拡大
            transform.localScale = new Vector3(_scaleValue, _scaleValue, _scaleValue);
            transform.Rotate(0, _rotateSpeed, 0);
        }

        _scaleValue += 0.5f + Time.deltaTime;


    }
}
