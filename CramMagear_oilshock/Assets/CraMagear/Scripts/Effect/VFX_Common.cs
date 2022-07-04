using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_Common : MonoBehaviour
{
    [SerializeField] VisualEffect _effect;
    public VisualEffect Effect => _effect;

    public void Play()
    {
        _effect.SendEvent("Play");
    }

    public void Stop()
    {
        _effect.SendEvent("Stop");
    }

    public void SetPlayRate(float rate)
    {
        _effect.playRate = rate;
    }

    public void SetBool(bool flag, string name = "Bool")
    {
        _effect.SetBool(name, flag);
    }

    public bool GetBool(string name = "Bool")
    {
        return _effect.GetBool(name);
    }

    public void SetFloat(float vol, string name = "Float")
    {
        _effect.SetFloat(name, vol);
    }

    public float GetFloat(string name = "Float")
    {
        return _effect.GetFloat(name);
    }

    public void SetVector3(Vector3 vec, string name = "Vector3")
    {
        _effect.SetVector3(name, vec);
    }

    public Vector3 GetVector3(string name = "Vector3")
    {
        return _effect.GetVector3(name);
    }

    public void SetVector4(Vector3 vec, string name = "Vector4")
    {
        _effect.SetVector4(name, vec);
    }

    public Vector4 GetVector4(string name = "Vector4")
    {
        return _effect.GetVector4(name);
    }

    public void SetColor(Vector4 vec, string name = "Color")
    {
        _effect.SetVector4(name, vec);
    }

    public Color GetColor(string name = "Color")
    {
        return _effect.GetVector4(name);
    }

    public void SetGradient(Gradient gradient, string name = "Gradient")
    {
        _effect.SetGradient(name, gradient);
    }

    public void SetTexture(Texture texture, string name = "Texture")
    {
        _effect.SetTexture(name, texture);
    }

    public void SetMesh(Mesh mesh, string name = "Mesh")
    {
        _effect.SetMesh(name, mesh);
    }
}
