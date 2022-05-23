using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroupMemberInputProvider : InputProvider
{
    [SerializeField] private Transform _targetTransform;
    public Transform TargetTransform { set => _targetTransform = value; get => _targetTransform; }

    [SerializeField] private NavMeshAgent _navMeshAgent;

    //�����擾
    public override Vector2 GetAxisL()
    {
        if (_targetTransform == null)
        {
            return Vector2.zero;
        }

        _navMeshAgent.SetDestination(_targetTransform.position);

        Vector2 moveVec = new Vector2(_navMeshAgent.velocity.x, _navMeshAgent.velocity.z);
        moveVec.Normalize();

        return moveVec;
    }

    //�E���擾
    public override Vector2 GetAxisR() => Vector2.zero;

    //�U���{�^��
    public override bool GetButtonAttack()
    {
        return false;
    }
}
