using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _createTime = 1.0f;
    [SerializeField] private Transform _createObjPrefab;

    [SerializeField] private bool _isCreateDestroy = false;

    private float _count = 0.0f;

    // Start is called before the first frame update
    void Update()
    {
        _count += Time.deltaTime;
        if (_count > _createTime)
        {
            Instantiate(_createObjPrefab,transform.position,transform.rotation);
            _count = 0.0f;
            if(_isCreateDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
