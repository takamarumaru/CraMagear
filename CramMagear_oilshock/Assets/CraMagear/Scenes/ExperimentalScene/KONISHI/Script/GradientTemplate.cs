using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientTemplate : MonoBehaviour
{
    [Header("Gradient�̃e���v���[�g")]
    [SerializeField] List<Gradient> _gradients;

    uint _useIndex = 0;

    private void Start()
    {
        IsRightIndex(_useIndex);
    }

    /// <summary>
    /// �e���v���[�g��Gradients����index�Ŏw�肵�����̂��g��
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
            Debug.LogError("�w�肳�ꂽ UseIndex ���s���ł��B(GradientTemplate.cs)");
        }
    }
}
