using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageJudgeCastle : MonoBehaviour, IDamageApplicable
{
    MainObjectParameter _parameter;

    void Awake()
    {
        //parameter���擾
        _parameter = GetComponent<MainObjectParameter>();
    }

    bool IDamageApplicable.ApplyDamage(ref DamageParam param)
    {
        _parameter.hp -= param.DamageValue;
        Debug.Log("Hit" + _parameter.name);
        return true;
    }
}
