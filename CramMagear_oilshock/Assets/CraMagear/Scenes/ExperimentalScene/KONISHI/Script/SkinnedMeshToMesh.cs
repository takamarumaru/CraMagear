using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class SkinnedMeshToMesh : MonoBehaviour
{
    VFX_Common _vfxCommon;

    [SerializeField] SkinnedMeshRenderer _skinedMesh;

    private void Awake()
    {
        _vfxCommon = transform.GetComponent<VFX_Common>();
    }

    public void UpdateSkinnedMeshVFX()
    {
        Mesh mesh = new Mesh();
        _skinedMesh.BakeMesh(mesh);

        _vfxCommon.SetMesh(mesh);
    }
}
