using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotionCalculate : MonoBehaviour
{
    // �o�����W
    Vector3 _appearPos = new Vector3();

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
        _projectileMotion.GetParameters(out _appearPos, out _angleX, out _angleY, out _velocity, out _gravity, out _takeTime);
    }

    void Update()
    {
        transform.position = _projectileMotion.Calculate(_time, _angleX, _angleY, _velocity, _gravity);

        _time += _speed * Time.deltaTime;

        if (_time >= _takeTime)
        {
            Destroy(gameObject);
        }
    }
}
