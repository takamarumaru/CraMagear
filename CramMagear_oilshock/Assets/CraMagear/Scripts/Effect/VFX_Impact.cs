using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_Impact : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [Header("èâä˙íl")]
    [SerializeField] Gradient _gradient;
    [SerializeField] float _playRate;

    void Awake()
    {
        _vfxCommon = transform.gameObject.GetComponent<VFX_Common>();
        _vfxCommon.SetGradient(_gradient);
        _vfxCommon.Play();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    _vfxCommon.Play();
        //}

        //_vfxCommon.SetPlayRate(_playRate);
    }
}
