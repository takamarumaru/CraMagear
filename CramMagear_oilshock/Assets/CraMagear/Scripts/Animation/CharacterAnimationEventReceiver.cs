using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    //銃
    [SerializeField]
    [Tooltip("弾のスクリプト")] 
    private BulletShot _bulletShot;

    [SerializeField] CharacterBrain _brain;
    [SerializeField] Transform _templateObjects;

    Animator _animator;

    private void Awake()
    {
        TryGetComponent(out _animator);
    }

    //Root　Motionで移動などをした時に実行
    private void OnAnimatorMove()
    {
        _brain.Move(_animator.deltaPosition);
    }

    public void AnimEvent_BulletShot()
    {
        //弾生成
        _bulletShot.LauncherShot();

        //Debug.Log("弾発射！");
    }

    public void Tread_Attack(string objName)
    {
        Transform obj = _templateObjects.Find(objName);
        if (obj != null)
        {
            //複製         戻り値もある参照         transformは親子の検索でも使う
            var newObj = Instantiate(original: obj.gameObject, parent: _brain.transform);
        }
        // Debug.Log("攻撃判定！");
    }
}
