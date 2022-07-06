using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


public class BulletShot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("弾の発射場所")]
    private GameObject firingPoint;

    [SerializeField]
    [Tooltip("弾の発射場所の調整")]
    private Vector3 firingPointOffset;

    [SerializeField]
    [Tooltip("弾の最大距離の場所")]
    private GameObject MaxShotPoint;


    [System.Serializable]
    public class CreateBullet
    {
        public Transform bullet;
        public Transform muzzleflash;

    }
    [Tooltip("発射する弾情報のリスト")]
    [SerializeField] private List<CreateBullet> _createBulletList = new();
    private int _selectIdx = 0;

    private void Awake()
    {
        Debug.Assert(_createBulletList.Count != 0, "BulletShotの_createBulletListが空です。");
    }
    public void Update()
    {
        //デバッグ用の仮の処理、本来はPlayerのState等でSwitching()を呼び出す
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingLeft())
        {
            Switching(-1);
        }
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingRight())
        {
            Switching(1);
        }
    }

    // 弾の発射
    public void LauncherShot()
    {
        Vector3 offset = new Vector3();
        offset += firingPoint.transform.right * firingPointOffset.x;
        offset += firingPoint.transform.up * firingPointOffset.y;
        offset += firingPoint.transform.forward * firingPointOffset.z;

        // 弾を発射する場所を取得
        Vector3 bulletPosition = firingPoint.transform.position + offset;

        Quaternion shotRot;

        if (MaxShotPoint)
        {
            //最大距離までのベクトル
            Vector3 shotDir = MaxShotPoint.transform.position - bulletPosition;

            shotRot = Quaternion.LookRotation(shotDir, Vector3.right);
        }
        else
        {
            shotRot = firingPoint.transform.rotation;
        }

        // 上で取得した場所に、"bullet"のPrefabを出現させる
        Instantiate(_createBulletList[_selectIdx].bullet.gameObject ,bulletPosition, shotRot);

        // bulletPositionから位置を調整してマズルフラッシュを出す
        Instantiate(_createBulletList[_selectIdx].muzzleflash.gameObject, bulletPosition, shotRot);
    }

    //弾切り替え
    public void Switching(int difference)
    {
        int newIdx = _selectIdx + difference;
        //範囲制限
        if (newIdx < 0) newIdx = _createBulletList.Count - 1;
        _selectIdx = newIdx % _createBulletList.Count;
    }

    public void AddBullet(CreateBullet newBullet)
    {
        _createBulletList.Add(newBullet);
    }

}
