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
    [Tooltip("弾の最大距離の場所")]
    private GameObject MaxShotPoint;

    [SerializeField]
    [Tooltip("弾")]
    private GameObject bullet;

    /// <summary>
    /// 弾の発射
    /// </summary>
    public void LauncherShot()
    {
        // 弾を発射する場所を取得
        Vector3 bulletPosition = firingPoint.transform.position;

        //最大距離までのベクトル
        Vector3 shotDir = MaxShotPoint.transform.position - bulletPosition;

        Quaternion shotRot = Quaternion.LookRotation(shotDir, Vector3.right);

        // 上で取得した場所に、"bullet"のPrefabを出現させる
        Instantiate(bullet, bulletPosition, shotRot);
    }
}
