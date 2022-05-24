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
}


public interface IDamageApplicable
{
    bool ApplyDamage(ref DamageParam param);
}


public class DamageApplicant : MonoBehaviour
{
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


    [Header("�q�b�g�X���[")]
    [SerializeField] float _hitSlow = 1.0f;
    [Tooltip("��������")]
    [SerializeField] float _hitSlowDuration = 0.5f;
    [Tooltip("�J�n����")]
    [SerializeField] float _hitSlowStartDelayTime;

    public void Update()
    {
        //����
        _lifespan -= Time.deltaTime;
        if(_lifespan <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach(var contact in collision.contacts)
        {
            var dmgApp = collision.rigidbody.GetComponent<IDamageApplicable>();
            if(dmgApp != null)
            {
                DamageParam param = new DamageParam();
                param.DamageValue = _damage;
                param.HitStopDuration = 0;
                param.Blow = new Vector3();

                if(dmgApp.ApplyDamage(ref param))
                {
                    //�����������ɂ�肽������
                }
            }
        }
    }
}
