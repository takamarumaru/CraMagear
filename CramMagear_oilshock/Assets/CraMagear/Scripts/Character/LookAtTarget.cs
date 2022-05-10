using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    //�W�I���͈͓��ɂ��邩
    public bool isInTheRange = true;

    [Header("parameter")]

    [Tooltip("��b�ԂɒǏ]����p�x")]
    [SerializeField] private float _followingSpeed;

    [Header("component reference")]

    [Tooltip("���肷��Collider")]
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
        //�W�I�����Ȃ���������s���Ȃ�
        if (!_targetTransform) return;

        //��������v���C���[�܂ł̃x�N�g�����Z�o
        Vector3 vLook = _targetTransform.position - transform.position;
        //���g����]
        Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _followingSpeed * Time.deltaTime);

        //�W�I�Ƃ��Ă���Transform���͈͊O�Ɉړ�������null��
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
        ////�W�I�����ɂ���Ȃ�΋������Z�o
        //if (_targetTransform)
        //{
        //    _distanceToTarget = (_targetTransform.position - transform.position).magnitude;
        //}
        ////�V����Transform�̋������Z�o
        //float distanceToOther = (other.transform.position - transform.position).magnitude;
        //if (distanceToOther <= _distanceToTarget)
        //{
        //    _targetTransform = other.transform;
        //}
    }
}
