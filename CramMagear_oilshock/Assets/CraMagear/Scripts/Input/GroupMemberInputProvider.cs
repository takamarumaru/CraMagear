using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroupMemberInputProvider : InputProvider
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    //ç∂é≤éÊìæ
    public override Vector2 GetAxisL()
    {
        //Vector3 vectorToTheTarget = _targetTransform.position - transform.position;
        //vectorToTheTarget.y = 0.0f;

        //if (vectorToTheTarget.magnitude <= 1.5f)
        //{
        //    return new Vector2(0.0f, 0.0f);
        //}
        //vectorToTheTarget.Normalize();       

        //return new Vector2(vectorToTheTarget.x, vectorToTheTarget.z);

        _navMeshAgent.SetDestination(_targetTransform.position);

        Vector2 moveVec = new Vector2(_navMeshAgent.velocity.x, _navMeshAgent.velocity.z);
        moveVec.Normalize();

        return moveVec;
        //return Vector2.zero;
    }

    //âEé≤éÊìæ
    public override Vector2 GetAxisR() => Vector2.zero;

    //çUåÇÉ{É^Éì
    public override bool GetButtonAttack()
    {
        return false;
    }
}
