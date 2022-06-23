using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionJudge : MonoBehaviour
{
    MainObjectParameter _parameter;

    [Header("�����I�u�W�F�N�g")]
    [SerializeField] GameObject _prefabHitEffectObject;

    private void Awake()
    {
        _parameter = GetComponent<MainObjectParameter>();

        Debug.Assert(_parameter != null, "ExplosionJudge�̕t���Ă���I�u�W�F�N�g��MainObjectParameter���ݒ肳��Ă��܂���B");
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


                Debug.Assert(_prefabHitEffectObject != null, "ExplosionJudge�ɍU���̃I�u�W�F�N�g���ݒ肳��Ă��܂���B");

                //�����������ɂ�肽������
                Instantiate(_prefabHitEffectObject, transform.position, transform.rotation);

                Destroy(transform.gameObject);
            }
        }
    }
}
