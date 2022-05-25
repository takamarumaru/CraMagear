using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    //�e
    [SerializeField]
    [Tooltip("�e�̃X�N���v�g")] 
    private BulletShot _bulletShot;

    [SerializeField] CharacterBrain _brain;
    [SerializeField] Transform _templateObjects;

    Animator _animator;

    private void Awake()
    {
        TryGetComponent(out _animator);
    }

    //Root�@Motion�ňړ��Ȃǂ��������Ɏ��s
    private void OnAnimatorMove()
    {
        _brain.Move(_animator.deltaPosition);
    }

    public void AnimEvent_BulletShot()
    {
        //�e����
        _bulletShot.LauncherShot();

        //Debug.Log("�e���ˁI");
    }

    public void Tread_Attack(string objName)
    {
        Transform obj = _templateObjects.Find(objName);
        if (obj != null)
        {
            //����         �߂�l������Q��         transform�͐e�q�̌����ł��g��
            var newObj = Instantiate(original: obj.gameObject, parent: _brain.transform);
        }
        // Debug.Log("�U������I");
    }
}
