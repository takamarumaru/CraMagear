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

    [Tooltip("マズルフラッシュ")]
    [SerializeField] private GameObject _effect;

    [Tooltip("生成座標オフセット")]
    [SerializeField] private Vector3 _createOffset;

    private float count;

    [Header("component reference")]

    [Tooltip("特定のターゲットを見ているScript")]
    [SerializeField] private LookAtTarget _lookAtTarget;


    // Update is called once per frame
    void Update()
    {
        if (_lookAtTarget.isInTheRange == false) { return; }

        Vector3 offset = new Vector3();
        offset += transform.right * _createOffset.x;
        offset += transform.up * _createOffset.y;
        offset += transform.forward * _createOffset.z;

        count += Time.deltaTime;
        if (count > _createInterval)
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);

            // マズルフラッシュを出す
            if (_effect)
            {
                Instantiate(_effect, transform.position + offset, transform.rotation);
            }
            count = 0.0f;
        }
    }
}
