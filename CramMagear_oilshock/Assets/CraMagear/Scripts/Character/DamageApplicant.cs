using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageParam
{
    public int DamageValue;         //�_���[�W�l
    public float HitStopDuration;   //�q�b�g�X�g�b�v����
    public Vector3 Blow;            //�Ԃ���΂���

    public bool Ret_IsGround;           //�h�䂵����
    public int Ret_ReflectDamageValue;  //���˃_���[�W

    public bool BlitzDamage;            //�d���_���[�W
}


public interface IDamageApplicable
{
    MainObjectParameter MainObjectParam { get; }
    bool ApplyDamage(ref DamageParam param);
}


public class DamageApplicant : MonoBehaviour
{
    [SerializeField] MainObjectParameter _parameter;

    [Tooltip("�U�����莝������")]
    [SerializeField] float _lifespan = 1.0f;
    [Tooltip("�_���[�W")]
    [SerializeField] int _damage = 1;
    [Tooltip("�_���[�W�䗦")]
    [SerializeField] float _damageRate = 0.1f;
    [Tooltip("�q�b�g�Ԋu")]
    [SerializeField] float _hitInterval = 0.1f;

    [Header("�q�b�g�X�g�b�v")]
    [SerializeField] float _hitDuration = 0.5f;

    [Header("�d���U�����ǂ���")]
    [SerializeField] bool _blitzAttack = false;

    [Header("�q�b�g�X���[")]
    [SerializeField] float _hitSlow = 1.0f;
    [Tooltip("��������")]
    [SerializeField] float _hitSlowDuration = 0.5f;
    [Tooltip("�J�n����")]
    [SerializeField] float _hitSlowStartDelayTime;

    [SerializeField] GameObject _prefabHitEffectObject;

    private void Awake()
    {
        Debug.Assert(_parameter != null, "DamageApplicant��MainObjectParameter���ݒ肳��Ă��܂���B");
        Debug.Assert(_prefabHitEffectObject != null, "DamageApplicant�ɍU�����̃G�t�F�N�g���ݒ肳��Ă��܂���B");
    }
    public void Update()
    {
        //����
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

                //�d���U�����ǂ���
                if (_blitzAttack)
                {
                    param.BlitzDamage = _blitzAttack;
                    Debug.Log("�d���_���[�W");
                }

                if (dmgApp.ApplyDamage(ref param))
                {
                    //�����������ɂ�肽������
                    Instantiate(_prefabHitEffectObject, contact.point, transform.rotation);

                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
