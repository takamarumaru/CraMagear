using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Homing : MonoBehaviour
{
    [SerializeField, Header("�W�I�̃I�u�W�F�N�g")]
    GameObject _targetObj;

    // offset���܂߂����W
    Vector3 _targetPos;

    [SerializeField, Header("�o���n�_�����΂��p�x�͈̔�")]
    float _angle;

    [SerializeField, Header("��΂����x")]
    float _launchSpeed;

    [SerializeField, Header("�g���C�[�W���O")]
    Ease _easing;

    [SerializeField, Header("�z�[�~���O�̋���")]
    float _rate;

    [SerializeField, Header("�z�[�~���O�̑��x")]
    float _speed;

    [SerializeField, Header("��������Ă���z�[�~���O����܂ł̎���")]
    float _homingWaitTimer;

    [SerializeField, Header("�z�[�~���O���߂Â��ē��������Ƃ��鋗��")]
    float _hitThreshold = 0.5f;

    [SerializeField]
    Vector3 _forward;

    Vector3 _launchVec;

    // �������Ă��������܂ł̎���
    float _lifetimeByHit = 0;

    bool _isHitTarget = false;

    [SerializeField, Header("�G�t�F�N�g")]
    GameObject _effectObj;

    [SerializeField, Header("�����������ɏo���G�t�F�N�g")]
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
