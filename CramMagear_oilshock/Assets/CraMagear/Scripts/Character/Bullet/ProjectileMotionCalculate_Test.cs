using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotionCalculate_Test : MonoBehaviour
{
    // 出現座標
    Vector3 _appearPos = new Vector3(10.0f,0.0f,10.0f);
    [SerializeField] GameObject _PlayerPos;

    // 角度
    float _angleX = 0;
    float _angleY = 0;

    //初速度
    float _velocity = 0;

    //重力加速度
    float _gravity = 0;

    //掛かる時間
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
