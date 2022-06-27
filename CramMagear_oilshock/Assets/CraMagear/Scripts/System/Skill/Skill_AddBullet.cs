using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_AddBullet : SkillBase
{
    [Header("SubClass Settings")]
    [Tooltip("�e�e���Ǘ����Ă���R���|�[�l���g")]
    [SerializeField] BulletShot _bulletManager;
    [Tooltip("���₷�e�e�̏��")]
    [SerializeField] BulletShot.CreateBullet _bullet;

    public override void Invoke()
    {
        //�K�v�ȏ�񂪓��͂���Ă��Ȃ��ꍇ
        if (_bulletManager == null || _bullet == null)
        {
            Debug.LogAssertion("[Skill_AddBullet]inspector�ɏ�񂪓��͂���Ă��Ȃ��\��������܂��B");
            return;
        }
        //���z�Ǘ��҂Ɍ��z����ǉ�
        _bulletManager.AddBullet(_bullet);
        return;
    }
}
