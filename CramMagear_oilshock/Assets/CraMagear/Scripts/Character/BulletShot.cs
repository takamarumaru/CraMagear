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


    [System.Serializable]
    public class CreateBullet
    {
        public Transform bullet;
        public Transform muzzleflash;
        public float recastTime;
        public bool isCurveBullet;
        [HideInInspector] public float recastCount;

    }
    [Tooltip("���˂���e���̃��X�g")]
    [SerializeField] private List<CreateBullet> _createBulletList = new();
    private int _selectIdx = 0;

    [Tooltip("�Ȏ�UI")]
    [SerializeField] private GameObject _curveBulletUIObject;

    private void Awake()
    {
        Debug.Assert(_createBulletList.Count != 0, "BulletShot��_createBulletList����ł��B");
    }
    public void Update()
    {
        //�f�o�b�O�p�̉��̏����A�{����Player��State����Switching()���Ăяo��
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingLeft())
        {
            Switching(-1);
        }
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingRight())
        {
            Switching(1);
        }

        foreach (CreateBullet bullet in _createBulletList)
        {
            bullet.recastCount += Time.deltaTime;
        }
        if (_curveBulletUIObject)
        {
            _curveBulletUIObject.SetActive(_createBulletList[_selectIdx].isCurveBullet);
        }
    }

    // �e�̔���
    public void LauncherShot()
    {
        //���L���X�g���Ԃ��o�܂œ����Ă��Ȃ�
        CreateBullet nowBullet = _createBulletList[_selectIdx];
        if (nowBullet.recastCount < nowBullet.recastTime)
        {
            return;
        }
        nowBullet.recastCount = 0;

        //�I�t�Z�b�g�ݒ�
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
        Instantiate(_createBulletList[_selectIdx].bullet.gameObject ,bulletPosition, shotRot);

        // bulletPosition����ʒu�𒲐����ă}�Y���t���b�V�����o��
        Instantiate(_createBulletList[_selectIdx].muzzleflash.gameObject, bulletPosition, shotRot);
    }

    //�e�؂�ւ�
    public void Switching(int difference)
    {
        int newIdx = _selectIdx + difference;
        //�͈͐���
        if (newIdx < 0) newIdx = _createBulletList.Count - 1;
        _selectIdx = newIdx % _createBulletList.Count;
    }

    public void AddBullet(CreateBullet newBullet)
    {
        _createBulletList.Add(newBullet);
    }

}
