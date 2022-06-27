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

    public void SetFloat(float vol, string name = "float")
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
