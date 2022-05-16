using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    //èe
    [SerializeField]
    [Tooltip("íeÇÃÉXÉNÉäÉvÉg")] 
    private BulletShot _bulletShot;

    public void AnimEvent_BulletShot()
    {
        //íeê∂ê¨
        _bulletShot.LauncherShot();

        Debug.Log("íeî≠éÀÅI");
    }
}
