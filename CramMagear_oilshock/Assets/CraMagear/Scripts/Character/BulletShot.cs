using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


public class BulletShot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("’e‚Ì”­ËêŠ")]
    private GameObject firingPoint;

    [SerializeField]
    [Tooltip("’e‚ÌÅ‘å‹——£‚ÌêŠ")]
    private GameObject MaxShotPoint;

    [SerializeField]
    [Tooltip("’e")]
    private GameObject bullet;

    /// <summary>
    /// ’e‚Ì”­Ë
    /// </summary>
    public void LauncherShot()
    {
        // ’e‚ğ”­Ë‚·‚éêŠ‚ğæ“¾
        Vector3 bulletPosition = firingPoint.transform.position;

        Quaternion shotRot;

        if (MaxShotPoint)
        {
            //Å‘å‹——£‚Ü‚Å‚ÌƒxƒNƒgƒ‹
            Vector3 shotDir = MaxShotPoint.transform.position - bulletPosition;

            shotRot = Quaternion.LookRotation(shotDir, Vector3.right);
        }
        else
        {
            shotRot = firingPoint.transform.rotation;
        }

        // ã‚Åæ“¾‚µ‚½êŠ‚ÉA"bullet"‚ÌPrefab‚ğoŒ»‚³‚¹‚é
        Instantiate(bullet, bulletPosition, shotRot);
    }
}
