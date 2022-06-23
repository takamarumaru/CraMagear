using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRayController : MonoBehaviour
{
    private VFX_Common _vfxCommon;
    [SerializeField] private float _lerpTime;
    private float _lerpMiddleTime = 0;

    [SerializeField] AnimationCurve _lerpAnim;

    private Vector3 _startForward;

    [SerializeField] float _impactDistanceInterval;
    private Vector3 _prevHitPoint = new Vector3(float.MaxValue,0,0);
    private float _hitPointDistance;

    [SerializeField] LayerMask _layerMask;

    [SerializeField] Transform _impact;

    void Awake()
    {
        _vfxCommon = transform.gameObject.GetComponent<VFX_Common>();
        _startForward = transform.forward;
    }
    private void Update()
    {
        //laser�J�ڎ��ԕ��o�߂�����Ԃ�
        if (_lerpMiddleTime > _lerpTime) return;

        float lerpMiddle = _lerpAnim.Evaluate(Mathf.Clamp01(_lerpMiddleTime / _lerpTime));

        //down��������O�����܂ł̒���Vector�����߂Ă��̕����ɉ�]
        Vector3 layDirection = Vector3.Lerp(-Vector3.up, _startForward, lerpMiddle);
        layDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(layDirection);

        //���ԉ��Z
        _lerpMiddleTime += Time.deltaTime;
        //���C���肵�ē������Ă����瓖�������n�_���G�t�F�N�g�̍ŏI�n�_�Ƃ���
        Ray ray = new Ray(transform.position, layDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            //���������n�_�Ƀ��[�U�[���ړ�
            _vfxCommon.SetVector3(new Vector3(0,0,hit.distance), "LaserDstPos");

            //�O�񓖂������n�_���G���[�l�Ȃ�hit.point�Ɠ����ɂ���
            if (Mathf.Approximately(_prevHitPoint.x,float.MaxValue))
            {
                _prevHitPoint = hit.point;
            }
            //�O�񓖂������ʒu�ƍ��񓖂������ʒu�Ƃ̋������Z�o�����Z
            _hitPointDistance += (hit.point - _prevHitPoint).magnitude;
            //��苗���ȏ�ɂȂ�Δ����G�t�F�N�g�𐶐�
            if(_hitPointDistance >= _impactDistanceInterval)
            {
                Instantiate(_impact, hit.point, transform.rotation);
                _hitPointDistance = 0.0f;
            }
            _prevHitPoint = hit.point;
        }
    }
}
