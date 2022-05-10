using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("parameter")]

    [Tooltip("発射する間隔")]
    [SerializeField] private float _createInterval;

    [Tooltip("生成する弾")]
    [SerializeField] private GameObject _bulletPrefab;

    [Tooltip("生成座標オフセット")]
    [SerializeField] private float _createOffset;

    private float count;

    [Header("component reference")]

    [Tooltip("特定のターゲットを見ているScript")]
    [SerializeField] private LookAtTarget _lookAtTarget;


    // Update is called once per frame
    void Update()
    {
        if (_lookAtTarget.isInTheRange == false) { return; }

        count += Time.deltaTime;
        if(count > _createInterval)
        {
            Instantiate(_bulletPrefab,transform.position, transform.rotation);
            count = 0.0f;
        }
    }
}
