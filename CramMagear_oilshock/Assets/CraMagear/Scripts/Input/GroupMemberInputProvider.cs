using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroupMemberInputProvider : InputProvider
{
    [SerializeField] private Transform _targetTransform;
    public Transform TargetTransform { set => _targetTransform = value; get => _targetTransform; }

    [SerializeField] private NavMeshAgent _navMeshAgent;

    [SerializeField] private LookAtTarget lookAtTarget;

    private void Awake()
    {
        //_navMeshAgent.updatePosition = false;
        //_navMeshAgent.updateRotation = false;
    }

    //ç∂é≤éÊìæ
    public override Vector2 GetAxisL()
    {
        if (_targetTransform == null) return Vector2.zero;
        _navMeshAgent.SetDestination(_targetTransform.position);
        Vector2 moveVec = new Vector2(_navMeshAgent.velocity.x, _navMeshAgent.velocity.z);
        moveVec.Normalize();
        return moveVec;

        //_navMeshAgent.SetDestination(_targetTransform.position);

        //Vector3 target = new();
        //if (_navMeshAgent.path.corners.Length > 2)
        //{
        //    target = _navMeshAgent.path.corners[1];
        //}
        //else
        //{
        //    target = _targetTransform.position;
        //}

        //Vector3 moveVec = target - transform.position;
        //moveVec.y = 0.0f;
        //if(moveVec.magnitude <= _navMeshAgent.stoppingDistance)
        //{
        //    return Vector2.zero;
        //}
        //moveVec.Normalize();

        //_navMeshAgent.nextPosition = transform.position;

        //return new Vector2(moveVec.x, moveVec.z);
    }

    private void OnDrawGizmos()
    {
        //if (!_navMeshAgent) return;

        //Gizmos.color = Color.red;
        //Vector3 prepos = transform.position;
        //foreach (Vector3 pos in _navMeshAgent.path.corners)
        //{
        //    Gizmos.DrawLine(prepos, pos);
        //    prepos = pos;
        //}
    }

    //âEé≤éÊìæ
    public override Vector2 GetAxisR() => Vector2.zero;

    //çUåÇÉ{É^Éì
    public override bool GetButtonAttack()
    {
        return lookAtTarget.isInTheRange;
    }
}
