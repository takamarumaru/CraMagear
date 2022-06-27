using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeReference, SubclassSelector] List<SkillBase> _skillList;

    private void Awake()
    {
        //指定されたスキルをすべて実行する
        foreach(var skill in _skillList)
        {
            //continue条件
            //-スキルが設定されていない
            //-無効化されている
            if (skill == null || skill.isActive == false)
            { 
                continue; 
            }
            //実行
            skill.Invoke();
        }
    }
}
