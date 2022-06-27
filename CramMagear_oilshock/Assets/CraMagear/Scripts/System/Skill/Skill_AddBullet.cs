using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_AddBullet : SkillBase
{
    [Header("SubClass Settings")]
    [Tooltip("銃弾を管理しているコンポーネント")]
    [SerializeField] BulletShot _bulletManager;
    [Tooltip("増やす銃弾の情報")]
    [SerializeField] BulletShot.CreateBullet _bullet;

    public override void Invoke()
    {
        //必要な情報が入力されていない場合
        if (_bulletManager == null || _bullet == null)
        {
            Debug.LogAssertion("[Skill_AddBullet]inspectorに情報が入力されていない可能性があります。");
            return;
        }
        //建築管理者に建築物を追加
        _bulletManager.AddBullet(_bullet);
        return;
    }
}
