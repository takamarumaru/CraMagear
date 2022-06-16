using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageParam
{
    public int DamageValue;         //ダメージ値
    public float HitStopDuration;   //ヒットストップ時間
    public Vector3 Blow;            //ぶっ飛ばし力

    public bool Ret_IsGround;           //防御したよ
    public int Ret_ReflectDamageValue;  //反射ダメージ

    public bool BlitzDamage;            //電撃ダメージ
}


public interface IDamageApplicable
{
    MainObjectParameter MainObjectParam { get; }
    bool ApplyDamage(ref DamageParam param);
}


public class DamageApplicant : MonoBehaviour
{
    [SerializeField] MainObjectParameter _parameter;

    [Tooltip("攻撃判定持続時間")]
    [SerializeField] float _lifespan = 1.0f;
    [Tooltip("ダメージ")]
    [SerializeField] int _damage = 1;
    [Tooltip("ダメージ比率")]
    [SerializeField] float _damageRate = 0.1f;
    [Tooltip("ヒット間隔")]
    [SerializeField] float _hitInterval = 0.1f;

    [Header("ヒットストップ")]
    [SerializeField] float _hitDuration = 0.5f;

    [Header("電撃攻撃かどうか")]
    [SerializeField] bool _blitzAttack = false;

    [Header("ヒットスロー")]
    [SerializeField] float _hitSlow = 1.0f;
    [Tooltip("持続時間")]
    [SerializeField] float _hitSlowDuration = 0.5f;
    [Tooltip("開始時間")]
    [SerializeField] float _hitSlowStartDelayTime;

    [SerializeField] GameObject _prefabHitEffectObject;

    private void Awake()
    {
        Debug.Assert(_parameter != null, "DamageApplicantにMainObjectParameterが設定されていません。");
        Debug.Assert(_prefabHitEffectObject != null, "DamageApplicantに攻撃時のエフェクトが設定されていません。");
    }
    public void Update()
    {
        //寿命
        _lifespan -= Time.deltaTime;
        if (_lifespan <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            var rigidbody = collision.rigidbody;
            if (rigidbody == null)
            {
                //Debug.Log("aaa");
                //Debug.Log(collision.gameObject.name);

                continue;
            }

            var dmgApp = rigidbody.GetComponent<IDamageApplicable>();
            if (dmgApp != null)
            {
                if (_parameter.TeamID == dmgApp.MainObjectParam.TeamID) continue;
                DamageParam param = new DamageParam();
                param.DamageValue = _damage;
                param.HitStopDuration = 0;
                param.Blow = new Vector3();

                //電撃攻撃かどうか
                if (_blitzAttack)
                {
                    param.BlitzDamage = _blitzAttack;
                    Debug.Log("電撃ダメージ");
                }

                if (dmgApp.ApplyDamage(ref param))
                {
                    //当たった時にやりたい処理
                    Instantiate(_prefabHitEffectObject, contact.point, transform.rotation);

                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
