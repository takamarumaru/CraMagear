using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputProvider : InputProvider
{
    [SerializeField] private Transform _targetTransform;
    //�����擾
    public override Vector2 GetAxisL()
    {
        Vector3 vectorToTheTarget = _targetTransform.position - transform.position;
        vectorToTheTarget.y = 0.0f;

        if (vectorToTheTarget.magnitude <= 1.5f)
        {
            return new Vector2(0.0f, 0.0f);
        }
        vectorToTheTarget.Normalize();

        return new Vector2(vectorToTheTarget.x, vectorToTheTarget.z);
    }

    //�U���{�^��
    public override bool GetButtonAttack(){return false;}
}
