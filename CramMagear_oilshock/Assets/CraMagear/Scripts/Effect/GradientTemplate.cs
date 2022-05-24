using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientTemplate : MonoBehaviour
{
    [Header("Gradientのテンプレート")]
    [SerializeField] List<Gradient> _gradients;

    uint _useIndex = 0;

    private void Start()
    {
        IsRightIndex(_useIndex);
    }

    /// <summary>
    /// テンプレートのGradientsからindexで指定したものを使う
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Gradient SelectGradient(uint index)
    {
        IsRightIndex(index);

        return _gradients[(int)index];
    }

    void IsRightIndex(uint index)
    {
        if (index >= _gradients.Count)
        {
            Debug.LogError("指定された UseIndex が不正です。(GradientTemplate.cs)");
        }
    }
}
