using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


public class BulletShot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ")]
    private GameObject firingPoint;

    [SerializeField]
    [Tooltip("�e�̍ő勗���̏ꏊ")]
    private GameObject MaxShotPoint;

    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;

    /// <summary>
    /// �e�̔���
    /// </summary>
    public void LauncherShot()
    {
        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition = firingPoint.transform.position;

        //�ő勗���܂ł̃x�N�g��
        Vector3 shotDir = MaxShotPoint.transform.position - bulletPosition;

        Quaternion shotRot = Quaternion.LookRotation(shotDir, Vector3.right);

        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        Instantiate(bullet, bulletPosition, shotRot);
    }
}
