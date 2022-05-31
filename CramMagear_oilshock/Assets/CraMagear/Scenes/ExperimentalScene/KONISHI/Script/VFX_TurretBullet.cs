using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_TurretBullet : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField] Gradient _gradient;

    [SerializeField] Vector3 _forward;
    [SerializeField] float _speed;

    void Awake()
    {
        _vfxCommon = transform.gameObject.GetComponent<VFX_Common>();
        _vfxCommon.SetGradient(_gradient);
    }

    void Update()
    {
        //Vector3 localPos = _vfxCommon.GetVector3("LocalPos");
        //localPos += _forward * _speed * Time.deltaTime;
        //_vfxCommon.SetVector3(localPos, "LocalPos");
    }
}
