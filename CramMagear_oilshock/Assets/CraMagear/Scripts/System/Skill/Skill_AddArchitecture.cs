using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_AddArchitecture : SkillBase
{
    [Header("SubClass Settings")]
    [Tooltip("建築を管理しているコンポーネント")]
    [SerializeField] ArchitectureCreator _architectureCreator;
    [Tooltip("増やす建築物の情報")]
    [SerializeField] ArchitectureCreator.CreateArchitecture _addarchitecture;

    public override void Invoke()
    {
        //必要な情報が入力されていない場合
        if(_architectureCreator == null || _addarchitecture == null)
        {
            Debug.LogAssertion("[Skill_AddArchitecture]inspectorに情報が入力されていない可能性があります。");
            return;
        }
        //建築管理者に建築物を追加
        _architectureCreator.AddArchitecture(_addarchitecture);
        return;
    }
}
