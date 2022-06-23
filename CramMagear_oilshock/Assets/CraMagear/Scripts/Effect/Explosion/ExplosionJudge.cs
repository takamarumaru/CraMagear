using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionJudge : MonoBehaviour
{
    MainObjectParameter _parameter;

    [Header("爆発オブジェクト")]
    [SerializeField] GameObject _prefabHitEffectObject;

    private void Awake()
    {
        _parameter = GetComponent<MainObjectParameter>();

        Debug.Assert(_parameter != null, "ExplosionJudgeの付いているオブジェクトにMainObjectParameterが設定されていません。");
    }


    public void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            var rigidbody = collision.rigidbody;
            if (rigidbody == null)
            {
                continue;
            }

            var dmgApp = rigidbody.GetComponent<IDamageApplicable>();
            if (dmgApp != null)
            {
                if (_parameter.TeamID == dmgApp.MainObjectParam.TeamID) continue;


                Debug.Assert(_prefabHitEffectObject != null, "ExplosionJudgeに攻撃のオブジェクトが設定されていません。");

                //当たった時にやりたい処理
                Instantiate(_prefabHitEffectObject, transform.position, transform.rotation);

                Destroy(transform.gameObject);
            }
        }
    }
}
