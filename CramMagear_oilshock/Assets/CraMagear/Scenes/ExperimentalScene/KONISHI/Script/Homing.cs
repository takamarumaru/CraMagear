using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Homing : MonoBehaviour
{
    [SerializeField, Header("標的のオブジェクト")]
    GameObject _targetObj;

    // offsetも含めた座標
    Vector3 _targetPos;

    [SerializeField, Header("出現地点から飛ばす角度の範囲")]
    float _angle;

    [SerializeField, Header("飛ばす速度")]
    float _launchSpeed;

    [SerializeField, Header("使うイージング")]
    Ease _easing;

    [SerializeField, Header("ホーミングの強さ")]
    float _rate;

    [SerializeField, Header("ホーミングの速度")]
    float _speed;

    [SerializeField, Header("生成されてからホーミングするまでの時間")]
    float _homingWaitTimer;

    [SerializeField, Header("ホーミングが近づいて当たったとする距離")]
    float _hitThreshold = 0.5f;

    [SerializeField]
    Vector3 _forward;

    Vector3 _launchVec;

    // 当たってから消えるまでの時間
    float _lifetimeByHit = 0;

    bool _isHitTarget = false;

    [SerializeField, Header("エフェクト")]
    GameObject _effectObj;

    [SerializeField, Header("当たった時に出すエフェクト")]
    GameObject _getItemEffectObj;

    [SerializeField]
    Vector3 _offsetPos;

    void Awake()
    {
        var obj = new GameObject();
        obj.transform.position = Vector3.up;
        obj.transform.RotateAround(Vector3.zero, Vector3.right, Random.value * _angle);
        obj.transform.RotateAround(Vector3.zero, Vector3.up, Random.value * 360);
        _launchVec = obj.transform.position;
        Destroy(obj);

        transform.DOMove(transform.position + _launchVec.normalized * _launchSpeed, _homingWaitTimer).SetEase(_easing);
    }

    void Update()
    {
        _targetPos = _targetObj.transform.position + _offsetPos;

        if (_homingWaitTimer > 0)
        {
            LookAtTarget();

            _homingWaitTimer -= Time.deltaTime;
            return;
        }

        UpdateHoming();

        if ((_targetPos - transform.position).sqrMagnitude <= _hitThreshold * _hitThreshold)
        {
            _isHitTarget = true;
            _effectObj?.GetComponent<VFX_Homing>().HitTarget();
        }

        if (_isHitTarget)
        {
            transform.position = _targetPos;
            _lifetimeByHit -= Time.deltaTime;

            if (_lifetimeByHit <= 0)
            {
                Instantiate(_getItemEffectObj).SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    void LookAtTarget()
    {
        Vector3 dir = _targetPos - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        Quaternion offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        transform.rotation = lookAtRotation * offsetRotation;
    }

    void UpdateHoming()
    {
        Vector3 diffVec = (_targetPos - transform.position).normalized;
        Quaternion qEndAng = Quaternion.FromToRotation(Vector3.up, diffVec);
        Quaternion qAng = Quaternion.Slerp(transform.rotation, qEndAng, _rate);
        Vector3 dir = transform.rotation * Vector3.up;
        Vector3 pos = transform.position + dir * _speed;
        transform.SetPositionAndRotation(pos, qAng);
    }

    public void SetLifetime(float time)
    {
        _lifetimeByHit = time;
    }
}
