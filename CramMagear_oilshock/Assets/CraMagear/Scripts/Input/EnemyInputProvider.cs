using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInputProvider : InputProvider
{
    //AI����Z�b�g�����f�[�^
    public Vector2 AxisL { get; set; }
    public Vector2 AxisR { get; set; }
    public bool Attack { get; set; } = false;

    [SerializeField] private Transform _targetTransform;
    public Transform TargetTransform { set { _targetTransform = value; } }

    //
    [Header("���_�ɍU�����J�n����܂ł̋���")]
    [SerializeField] float _distance = 0;

    private NavMeshAgent _navMeshAgent;


    bool _isHitRange = false;

    [SerializeField] private LookAtTarget _lookAtTarget;


    private void Awake()
    {
        Debug.Assert(_targetTransform != null, "EnemyInputProvider��TargetTransform���ݒ肳��Ă��܂���B");
        Debug.Assert(_lookAtTarget != null, "EnemyInputProvider��LookAtTarget���ݒ肳��Ă��܂���B");
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
    }

    //�����擾
    public override Vector2 GetAxisL() => AxisL;

    //�ړ�������T��
    public Vector2 SearchTargetDirection()
    {
        if (_isHitRange == true) return Vector2.zero;
        if (_targetTransform == null) return Vector2.zero;

        _navMeshAgent.SetDestination(_targetTransform.position);

        Vector3 target = new();
        if (_navMeshAgent.path.corners.Length > 2)
        {
            target = _navMeshAgent.path.corners[1];
        }
        else
        {
            target = _targetTransform.position;
        }

        Vector3 moveVec = target - transform.position;
        moveVec.y = 0.0f;
        moveVec.Normalize();

        _navMeshAgent.nextPosition = transform.position;

        return new Vector2(moveVec.x, moveVec.z);
    }

    private void OnDrawGizmos()
    {
        if (!_navMeshAgent) return;

        Gizmos.color = Color.red;
        Vector3 prepos = transform.position;
        foreach (Vector3 pos in _navMeshAgent.path.corners)
        {
            Gizmos.DrawLine(prepos, pos);
            prepos = pos;
        }
    }

    //�U���{�^��
    public override bool GetButtonAttack()
    {
        //
        if (_lookAtTarget.isInTheRange)
        {
            return true;
        }

        //���_�܂ł̃x�N�g��
        Vector3 vectorToTheTarget = _targetTransform.position - transform.position;
        vectorToTheTarget.y = 0.0f;

        //�U���͈͓���������
        if (_isHitRange = (vectorToTheTarget.magnitude < _distance))
        {
            return true;
        }

        return false;
    }
}
