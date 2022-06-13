using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_MuzzleFlash : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField] Gradient _gradient;

    void Awake()
    {
        _vfxCommon = transform.gameObject.GetComponent<VFX_Common>();
        _vfxCommon.SetGradient(_gradient);
    }
}
