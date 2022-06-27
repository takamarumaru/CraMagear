using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_AddMember : SkillBase
{
    [Header("SubClass Settings")]
    [Tooltip("メンバーを管理しているコンポーネント")]
    [SerializeField] MembersAdministrator _membersAdministrator;
    [Tooltip("増やすメンバーの数")]
    [SerializeField] int _addMemberNum;

    public override void Invoke()
    {
        //必要な情報が入力されていない場合
        if (_membersAdministrator == null)
        {
            Debug.LogAssertion("[Skill_AddMember]inspectorに情報が入力されていない可能性があります。");
            return;
        }
        //建築管理者に建築物を追加
        _membersAdministrator.AddMamber(_addMemberNum);
        return;
    }
}
