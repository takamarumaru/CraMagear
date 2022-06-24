using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleRotate : MonoBehaviour
{
    [SerializeField] float _MaxScale = 0.0f;
    [SerializeField] float _rotateSpeed = 0.0f;

    [Header("爆発範囲セット")]
    [SerializeField] GameObject _explosionRange;

    //スケールの値
    float _scaleValue = 0.1f;

    //出現場所保存用
    public Vector3 _popPosition { get; set; }

    //AddComponentしたときに使う用（魔法陣の時）--------------------------
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
        //出現場所に出すため
        transform.position = _popPosition;

        //拡大処理
        if (_MaxScale < _scaleValue)
        {
            _scaleValue = _MaxScale;

            Debug.Assert(_explosionRange != null, "ExplosionにExplosionRangeが設定されていません。");

            //爆発範囲作成
            Instantiate(_explosionRange, transform.position, transform.rotation);

            //広がり切ったら魔法陣を破壊
            Destroy(gameObject);

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
