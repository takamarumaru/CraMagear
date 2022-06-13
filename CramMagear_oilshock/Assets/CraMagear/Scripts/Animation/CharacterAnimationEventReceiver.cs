using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    //銃
    [SerializeField]
    [Tooltip("弾のスクリプト")]
    private BulletShot _bulletShot;

    [SerializeField] CharacterBrain _brain;
    [SerializeField] Transform _templateObjects;
    [SerializeField] private UnityEvent _events;

    Animator _animator;

    private void Awake()
    {
        Debug.Assert(_brain != null, "CharacterAnimationReceiverにCharacterBrainが設定されていません。");
        Debug.Assert(_templateObjects != null, "CharacterAnimationReceiverにtemplateObjectsが設定されていません。");
        //Debug.Assert(_bulletShot != null, "プレイヤーかメンバーのCharacterAnimationReceiverに_bulletShotが設定されていません。");

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
        if(_events != null)
        {
            _events.Invoke();
        }

        //Debug.Log("弾発射！");
    }

    //敵の攻撃
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

    //キャラクターが倒れてから消える
    public void CharacterDown()
    {
        Destroy(_brain.transform.gameObject);
    }
}
