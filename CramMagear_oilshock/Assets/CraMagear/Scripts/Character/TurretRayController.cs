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
        //laser遷移時間分経過したら返る
        if (_lerpMiddleTime > _lerpTime) return;

        float lerpMiddle = _lerpAnim.Evaluate(Mathf.Clamp01(_lerpMiddleTime / _lerpTime));

        //down方向から前方向までの中間Vectorを求めてその方向に回転
        Vector3 layDirection = Vector3.Lerp(-Vector3.up, _startForward, lerpMiddle);
        layDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(layDirection);

        //時間加算
        _lerpMiddleTime += Time.deltaTime;
        //レイ判定して当たっていたら当たった地点をエフェクトの最終地点とする
        Ray ray = new Ray(transform.position, layDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            //当たった地点にレーザーを移動
            _vfxCommon.SetVector3(new Vector3(0,0,hit.distance), "LaserDstPos");

            //前回当たった地点がエラー値ならhit.pointと同じにする
            if (Mathf.Approximately(_prevHitPoint.x,float.MaxValue))
            {
                _prevHitPoint = hit.point;
            }
            //前回当たった位置と今回当たった位置との距離を算出し加算
            _hitPointDistance += (hit.point - _prevHitPoint).magnitude;
            //一定距離以上になれば爆発エフェクトを生成
            if(_hitPointDistance >= _impactDistanceInterval)
            {
                Instantiate(_impact, hit.point, transform.rotation);
                _hitPointDistance = 0.0f;
            }
            _prevHitPoint = hit.point;
        }
    }
}
