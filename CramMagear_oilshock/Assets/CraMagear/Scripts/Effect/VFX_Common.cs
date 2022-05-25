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

    public void SetGradient(Gradient gradient)
    {
        _effect.SetGradient("Gradient", gradient);
    }
}
