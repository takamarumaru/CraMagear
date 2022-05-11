using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("parameter")]

    [Tooltip("���˂���Ԋu")]
    [SerializeField] private float _createInterval;

    [Tooltip("��������e")]
    [SerializeField] private GameObject _bulletPrefab;

    [Tooltip("�������W�I�t�Z�b�g")]
    [SerializeField] private float _createOffset;

    private float count;

    [Header("component reference")]

    [Tooltip("����̃^�[�Q�b�g�����Ă���Script")]
    [SerializeField] private LookAtTarget _lookAtTarget;


    // Update is called once per frame
    void Update()
    {
        if (_lookAtTarget.isInTheRange == false) { return; }

        count += Time.deltaTime;
        if(count > _createInterval)
        {
            Instantiate(_bulletPrefab,transform.position, transform.rotation);
            count = 0.0f;
        }
    }
}
