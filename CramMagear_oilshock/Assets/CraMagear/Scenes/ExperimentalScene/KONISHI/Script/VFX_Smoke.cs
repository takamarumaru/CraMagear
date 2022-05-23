using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_Smoke : MonoBehaviour
{
    [System.Serializable]
    public struct _Gradient
    {
        [SerializeField] public Color _color;
        [SerializeField] public float _time;
    }

    [SerializeField] VisualEffect _effect;

    [Header("RGBAと時間(0〜1の割合)\n上から順に")]
    [SerializeField] List<_Gradient> _gradients;

    private GradientColorKey[] _colorKeys;
    private GradientAlphaKey[] _alphaKeys;

    private float _verocityY;

    // Start is called before the first frame update
    void Start()
    {
        SetGradient(_gradients);

        _verocityY = _effect.GetFloat("VelocityY");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        _effect.SendEvent("Play");
    }

    public void Stop()
    {
        _effect.SendEvent("Stop");
    }

    /// <summary>
    /// Gradientの詳細な設定
    /// </summary>
    /// <param name="gradients"></param>
    public void SetGradient(List<_Gradient> gradients)
    {
        int keyNum = gradients.Count;

        if (!IsRightKeyNum(keyNum))
        {
            return;
        }

        _colorKeys = new GradientColorKey[keyNum];
        _alphaKeys = new GradientAlphaKey[keyNum];

        for (int i = 0; i < keyNum; i++)
        {
            _colorKeys[i].color = gradients[i]._color;
            _colorKeys[i].time = gradients[i]._time;

            _alphaKeys[i].alpha = gradients[i]._color.a;
            _alphaKeys[i].time = gradients[i]._time;
        }

        Gradient _gradient = new Gradient();

        _gradient.SetKeys(_colorKeys, _alphaKeys);
        _effect.SetGradient("Color", _gradient);
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
        _effect.SetGradient("Color", gradient);
    }

    public void SetPlayRate(float rate)
    {
        _effect.playRate = rate;
    }

    public void SetVectorYPower(float pow)
    {
        _effect.SetFloat("VelocityY", _verocityY * pow);
    }

    /// <summary>
    /// keyNumが正しい範囲内か
    /// </summary>
    /// <param name="keyNum"></param>
    /// <returns></returns>
    bool IsRightKeyNum(int keyNum)
    {
        if (keyNum <= 0)
        {
            return false;
        }
        if (keyNum >= 8)
        {
            Debug.LogError("Gradientの最大数は 8 個です。現在 " + keyNum + " 個設定されています。");
            return false;
        }

        return true;
    }
}
