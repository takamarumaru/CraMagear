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
    [Tooltip("�e�̔��ˏꏊ�̒���")]
    private Vector3 firingPointOffset;

    [SerializeField]
    [Tooltip("�e�̍ő勗���̏ꏊ")]
    private GameObject MaxShotPoint;

    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("�}�Y���t���b�V��")]
    private GameObject effect;

    /// <summary>
    /// �e�̔���
    /// </summary>
    public void LauncherShot()
    {
        Vector3 offset = new Vector3();
        offset += firingPoint.transform.right * firingPointOffset.x;
        offset += firingPoint.transform.up * firingPointOffset.y;
        offset += firingPoint.transform.forward * firingPointOffset.z;

        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition = firingPoint.transform.position + offset;

        Quaternion shotRot;

        if (MaxShotPoint)
        {
            //�ő勗���܂ł̃x�N�g��
            Vector3 shotDir = MaxShotPoint.transform.position - bulletPosition;

            shotRot = Quaternion.LookRotation(shotDir, Vector3.right);
        }
        else
        {
            shotRot = firingPoint.transform.rotation;
        }

        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        Instantiate(bullet, bulletPosition, shotRot);

        // bulletPosition����ʒu�𒲐����ă}�Y���t���b�V�����o��
        Instantiate(effect, bulletPosition, shotRot);
    }
}
