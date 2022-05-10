using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    //標的が範囲内にいるか
    public bool isInTheRange = true;

    [Header("parameter")]

    [Tooltip("一秒間に追従する角度")]
    [SerializeField] private float _followingSpeed;

    [Header("component reference")]

    [Tooltip("判定するCollider")]
    [SerializeField] private SphereCollider _sphereCollider;

    private Transform _targetTransform;

    private float _distanceToTarget = float.MaxValue;


    void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        Vector3 point=new Vector3();
        _sphereCollider.ClosestPoint(point);
        //標的がいなかったら実行しない
        if (!_targetTransform) return;

        //自分からプレイヤーまでのベクトルを算出
        Vector3 vLook = _targetTransform.position - transform.position;
        //自身を回転
        Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _followingSpeed * Time.deltaTime);

        //標的としているTransformが範囲外に移動したらnullに
        isInTheRange = (vLook.magnitude <= _sphereCollider.radius);
        if (isInTheRange == false)
        {
            _targetTransform = null;
        }

        //_sphereCollider.
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag != "Enemy") return;
        ////標的が既にいるならば距離を算出
        //if (_targetTransform)
        //{
        //    _distanceToTarget = (_targetTransform.position - transform.position).magnitude;
        //}
        ////新たなTransformの距離を算出
        //float distanceToOther = (other.transform.position - transform.position).magnitude;
        //if (distanceToOther <= _distanceToTarget)
        //{
        //    _targetTransform = other.transform;
        //}
    }
}
