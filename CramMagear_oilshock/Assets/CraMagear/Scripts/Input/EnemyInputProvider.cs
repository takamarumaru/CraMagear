using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputProvider : InputProvider
{
    [SerializeField] private Transform _targetTransform;

    //
    [Header("拠点に攻撃を開始するまでの距離")]
    [SerializeField] float _distance = 0;

    //左軸取得
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

    //攻撃ボタン
    public override bool GetButtonAttack()
    {
        //拠点までのベクトル
        Vector3 vectorToTheTarget = _targetTransform.position - transform.position;
        vectorToTheTarget.y = 0.0f;

        //攻撃範囲内だったら
        if (vectorToTheTarget.magnitude < _distance)
        {
            Debug.Log("攻撃開始");
            return true;
        }


        return false;
    }
}
