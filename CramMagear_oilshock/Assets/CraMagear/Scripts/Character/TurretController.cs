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

    [Tooltip("�}�Y���t���b�V��")]
    [SerializeField] private GameObject _effect;

    [Tooltip("�������W�I�t�Z�b�g")]
    [SerializeField] private Vector3 _createOffset;

    private float count;

    [Header("component reference")]

    [Tooltip("����̃^�[�Q�b�g�����Ă���Script")]
    [SerializeField] private LookAtTarget _lookAtTarget;


    // Update is called once per frame
    void Update()
    {
        if (_lookAtTarget.isInTheRange == false) { return; }

        Vector3 offset = new Vector3();
        offset += transform.right * _createOffset.x;
        offset += transform.up * _createOffset.y;
        offset += transform.forward * _createOffset.z;

        count += Time.deltaTime;
        if (count > _createInterval)
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);

            // �}�Y���t���b�V�����o��
            if (_effect)
            {
                Instantiate(_effect, transform.position + offset, transform.rotation);
            }
            count = 0.0f;
        }
    }
}
