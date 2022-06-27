using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeReference, SubclassSelector] List<SkillBase> _skillList;

    private void Awake()
    {
        //�w�肳�ꂽ�X�L�������ׂĎ��s����
        foreach(var skill in _skillList)
        {
            //continue����
            //-�X�L�����ݒ肳��Ă��Ȃ�
            //-����������Ă���
            if (skill == null || skill.isActive == false)
            { 
                continue; 
            }
            //���s
            skill.Invoke();
        }
    }
}
