using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    //�e
    [SerializeField]
    [Tooltip("�e�̃X�N���v�g")] 
    private BulletShot _bulletShot;

    public void AnimEvent_BulletShot()
    {
        //�e����
        _bulletShot.LauncherShot();

        Debug.Log("�e���ˁI");
    }
}
