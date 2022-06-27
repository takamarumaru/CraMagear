using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_AddArchitecture : SkillBase
{
    [Header("SubClass Settings")]
    [Tooltip("���z���Ǘ����Ă���R���|�[�l���g")]
    [SerializeField] ArchitectureCreator _architectureCreator;
    [Tooltip("���₷���z���̏��")]
    [SerializeField] ArchitectureCreator.CreateArchitecture _addarchitecture;

    public override void Invoke()
    {
        //�K�v�ȏ�񂪓��͂���Ă��Ȃ��ꍇ
        if(_architectureCreator == null || _addarchitecture == null)
        {
            Debug.LogAssertion("[Skill_AddArchitecture]inspector�ɏ�񂪓��͂���Ă��Ȃ��\��������܂��B");
            return;
        }
        //���z�Ǘ��҂Ɍ��z����ǉ�
        _architectureCreator.AddArchitecture(_addarchitecture);
        return;
    }
}
