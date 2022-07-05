using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotionCalculate_Test : MonoBehaviour
{
    // �o�����W
    Vector3 _appearPos = new Vector3(10.0f,0.0f,10.0f);
    [SerializeField] GameObject _PlayerPos;

    // �p�x
    float _angleX = 0;
    float _angleY = 0;

    //�����x
    float _velocity = 0;

    //�d�͉����x
    float _gravity = 0;

    //�|���鎞��
    float _takeTime = 0;

    float _time = 0;

    [SerializeField] float _speed = 1;

    [SerializeField] GameObject _projectileMotionObj;
    ProjectileMotion _projectileMotion;

    void Awake()
    {
        _projectileMotion = _projectileMotionObj.GetComponent<ProjectileMotion>();
        _appearPos = _PlayerPos.transform.position;
        _projectileMotion.GetParameters(out _appearPos, out _angleX, out _angleY, out _velocity, out _gravity, out _takeTime);
    }

    void Update()
    {

        transform.position = _appearPos + _projectileMotion.Calculate(_time, _angleX, _angleY, _velocity, _gravity);

        _time += _speed * Time.deltaTime;

        if (_time >= _takeTime)
        {
            _PlayerPos.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    public float GetTime()
    {
        return _time;
    }
}
