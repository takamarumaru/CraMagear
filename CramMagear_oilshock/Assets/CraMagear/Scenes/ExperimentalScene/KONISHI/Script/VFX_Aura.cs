using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_Aura : MonoBehaviour
{
    VFX_Common _vfxCommon;
    public VFX_Common VFXCommon => _vfxCommon;

    void Awake()
    {
        _vfxCommon = transform.GetComponent<VFX_Common>();
    }

    void Update()
    {

    }
}
