using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_GetItem : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField, Header("エフェクトを出すオブジェクト")]
    GameObject _targetObj;

    [SerializeField]
    Vector3 _offsetPos;

    void Awake()
    {
        _vfxCommon = GetComponent<VFX_Common>();

        _vfxCommon.SetVector3(_targetObj.transform.position + _offsetPos, "Pos");

        Destroy(gameObject, _vfxCommon.GetFloat("Lifetime"));
    }

    void Update()
    {
        _vfxCommon.SetVector3(_targetObj.transform.position + _offsetPos, "Pos");
    }
}
