using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_Homing : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField, Header("�W�I�̃I�u�W�F�N�g")]
    GameObject _targetObj;

    // �G�t�F�N�g�̐e�I�u�W�F�N�g
    GameObject _parentObj;

    GameObject _followObj;

    Vector3 _localPos;

    bool _isHitTarget = false;

    void Awake()
    {
        _vfxCommon = GetComponent<VFX_Common>();

        _parentObj = transform.parent.gameObject;

        for (int i = 0; i < _parentObj.transform.childCount; i++)
        {
            var tmpObj = _parentObj.transform.GetChild(i).gameObject;
            if (tmpObj.tag != "Effect")
            {
                _followObj = tmpObj;
            }
        }

        _followObj.GetComponent<Homing>().SetLifetime(_vfxCommon.GetFloat("TrailLifetime"));

        _localPos = _followObj.transform.position;
        _vfxCommon.SetVector3(_localPos, "LocalPos");
    }

    void Update()
    {
        if (_followObj == null)
        {
            Destroy(gameObject);
            return;
        }

        _localPos = _followObj.transform.position;
        _vfxCommon.SetVector3(_localPos, "LocalPos");

        if (_isHitTarget)
        {
            _localPos = _targetObj.transform.position;
            _vfxCommon.SetFloat(_vfxCommon.GetFloat("TrailLifetime") - Time.deltaTime, "TrailLifetime");
        }

        if (_vfxCommon.GetFloat("TrailLifetime") <= 0)
        {
            Destroy(_followObj);
        }
    }

    public void HitTarget()
    {
        _isHitTarget = true;
    }
}
