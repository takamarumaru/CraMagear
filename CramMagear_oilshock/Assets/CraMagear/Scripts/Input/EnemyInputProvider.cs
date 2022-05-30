using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputProvider : InputProvider
{
    [SerializeField] private Transform _targetTransform;

    //
    [Header("���_�ɍU�����J�n����܂ł̋���")]
    [SerializeField] float _distance = 0;

    bool _isHitRange = false;

    //�����擾
    public override Vector2 GetAxisL()
    {
        if (_isHitRange)
        {
            return Vector2.zero;
        }

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
    public override bool GetButtonAttack()
    {
        //���_�܂ł̃x�N�g��
        Vector3 vectorToTheTarget = _targetTransform.position - transform.position;
        vectorToTheTarget.y = 0.0f;

        //�U���͈͓���������
        if (_isHitRange = (vectorToTheTarget.magnitude < _distance)) 
        {
            Debug.Log("�U���J�n");
            return true;
        }


        return false;
    }
}
