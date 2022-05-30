using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_Bullet : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField] Vector3 _forward;
    [SerializeField] float _speed;

    //[SerializeField] GameObject _obj;
    Vector3 _localPos;

    void Awake()
    {
        _vfxCommon = transform.gameObject.GetComponent<VFX_Common>();
       // _vfxCommon.Play();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    _vfxCommon.Play();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    _vfxCommon.Stop();
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    _vfxCommon.SetPlayRate(2);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _vfxCommon.SetPlayRate(1);
        //}

        //FixTransform();
        //SetLocalPos(transform.forward);
        Vector3 localPos = _vfxCommon.Effect.GetVector3("LocalPos");
        localPos += _forward * _speed * Time.deltaTime;
        _vfxCommon.Effect.SetVector3("LocalPos", localPos);
    }

    //void FixTransform()
    //{
    //    Vector3 pos = _obj.transform.position;
    //    Vector3 scale = _vfxCommon.Effect.GetVector3("Scale");
    //    _localPos = new Vector3(pos.x / scale.x, pos.y / scale.y, pos.z / scale.z);
    //}

    //public void SetLocalPos(Vector3 pos)
    //{
    //    _vfxCommon.Effect.SetVector3("LocalPos", pos);
    //}
}
