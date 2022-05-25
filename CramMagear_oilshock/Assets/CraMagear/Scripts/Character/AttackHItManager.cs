using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHItManager : MonoBehaviour
{
    MainObjectParameter _mainObjParam;

    //C++�ł���std::map
    // �q�b�g�����I�u�W�F�N�g�́A�c�莞�ԊǗ��}�b�v
    Dictionary<MainObjectParameter, float> _hitObjects = new();

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out _mainObjParam);
    }

    /// <summary>
    /// �o�^���X�g�ɑ��݂��邩�H
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Exist(MainObjectParameter obj)
    {
        return _hitObjects.ContainsKey(obj);
    }

    /// <summary>
    /// �o�^
    /// </summary>
    /// <param name="obj">�o�^����L�����N�^�[</param>
    /// <param name="duration">��������</param>
    public void Register(MainObjectParameter obj, float duration)
    {
        _hitObjects.Add(obj, duration);
        //_hitObjects[obj] = duration;
    }

    /// <summary>
    /// �o�^���X�g�����ׂăN���A����
    /// </summary>
    public void Reset()
    {
        _hitObjects.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //Dictionary�̃L�[�������A�z��ɃR�s�[����
        List<MainObjectParameter> tempList = new();
        foreach (var key in _hitObjects.Keys)
        {
            tempList.Add(key);
        }

        //�S���e����������
        foreach (var key in tempList)
        {
            float time = _hitObjects[key];
            time -= Time.deltaTime;
            _hitObjects[key] = time;

            //���Ԃ��s�������̂́A���X�g����폜
            if (time <= 0)
            {
                _hitObjects.Remove(key);
            }
        }
    }
}
