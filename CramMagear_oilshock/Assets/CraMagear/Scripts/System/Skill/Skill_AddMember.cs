using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_AddMember : SkillBase
{
    [Header("SubClass Settings")]
    [Tooltip("�����o�[���Ǘ����Ă���R���|�[�l���g")]
    [SerializeField] MembersAdministrator _membersAdministrator;
    [Tooltip("���₷�����o�[�̐�")]
    [SerializeField] int _addMemberNum;

    public override void Invoke()
    {
        //�K�v�ȏ�񂪓��͂���Ă��Ȃ��ꍇ
        if (_membersAdministrator == null)
        {
            Debug.LogAssertion("[Skill_AddMember]inspector�ɏ�񂪓��͂���Ă��Ȃ��\��������܂��B");
            return;
        }
        //���z�Ǘ��҂Ɍ��z����ǉ�
        _membersAdministrator.AddMamber(_addMemberNum);
        return;
    }
}
