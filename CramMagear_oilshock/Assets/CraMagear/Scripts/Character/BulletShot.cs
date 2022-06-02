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

    [SerializeField]
    [Tooltip("弾")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("マズルフラッシュ")]
    private GameObject effect;

    /// <summary>
    /// 弾の発射
    /// </summary>
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
        Instantiate(bullet, bulletPosition, shotRot);

        // bulletPositionから位置を調整してマズルフラッシュを出す
        Instantiate(effect, bulletPosition, shotRot);
    }
}
