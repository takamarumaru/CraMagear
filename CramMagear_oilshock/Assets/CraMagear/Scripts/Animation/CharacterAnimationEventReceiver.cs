using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    //�e
    [SerializeField]
    [Tooltip("�e�̃X�N���v�g")]
    private BulletShot _bulletShot;

    [SerializeField] CharacterBrain _brain;
    [SerializeField] Transform _templateObjects;
    [SerializeField] private UnityEvent _events;

    Animator _animator;

    private void Awake()
    {
        Debug.Assert(_brain != null, "CharacterAnimationReceiver��CharacterBrain���ݒ肳��Ă��܂���B");
        Debug.Assert(_templateObjects != null, "CharacterAnimationReceiver��templateObjects���ݒ肳��Ă��܂���B");
        //Debug.Assert(_bulletShot != null, "�v���C���[�������o�[��CharacterAnimationReceiver��_bulletShot���ݒ肳��Ă��܂���B");

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
        if(_events != null)
        {
            _events.Invoke();
        }

        //Debug.Log("�e���ˁI");
    }

    //�G�̍U��
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

    //�L�����N�^�[���|��Ă��������
    public void CharacterDown()
    {
        Destroy(_brain.transform.gameObject);
    }
}
