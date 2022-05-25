using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VFX_Common))]

public class VFX_Smoke : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField] Gradient _gradient;

    private float _verocityY;

    void Awake()
    {
        _vfxCommon = transform.gameObject.GetComponent<VFX_Common>();
        _vfxCommon.Play();

        _vfxCommon.SetGradient(_gradient);

        _verocityY = _vfxCommon.Effect.GetFloat("VelocityY");
    }

    void Update()
    {
        
    }

    /// <summary>
    /// １色と透明度だけを設定
    /// </summary>
    /// <param name="index">GradientTemplateのindex</param>
    /// <param name="color"></param>
    public void SetSimpleGradient(uint index, Color color)
    {
        //テンプレートから使う番号を選ぶ
        Gradient gradient = GameObject.Find("GradientTemplate").GetComponent<GradientTemplate>().SelectGradient(index);

        int colorKeyNum = gradient.colorKeys.Length;
        int alphaKeyNum = gradient.alphaKeys.Length;
        GradientColorKey[] colorKeys = new GradientColorKey[colorKeyNum];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[alphaKeyNum];

        for (int i = 0; i < colorKeyNum; i++)
        {
            colorKeys[i].color = color;
            colorKeys[i].time = i;
        }
        for (int i = 0; i < alphaKeyNum; i++)
        {
            alphaKeys[i].alpha = gradient.alphaKeys[i].alpha;
            alphaKeys[i].time = gradient.alphaKeys[i].time;
        }

        gradient.SetKeys(colorKeys, alphaKeys);
        _vfxCommon.Effect.SetGradient("Gradient", gradient);
    }

    public void SetVectorYPower(float pow)
    {
        _vfxCommon.Effect.SetFloat("VelocityY", _verocityY * pow);
    }
}
