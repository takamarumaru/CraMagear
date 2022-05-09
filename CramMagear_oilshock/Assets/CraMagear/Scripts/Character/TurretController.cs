using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("parameter")]

    [Tooltip("”­ŽË‚·‚éŠÔŠu")]
    [SerializeField] private float _createInterval;

    [Tooltip("¶¬‚·‚é’e")]
    [SerializeField] private GameObject _bulletPrefab;

    private float count;

    [Header("component reference")]

    [Tooltip("“Á’è‚Ìƒ^[ƒQƒbƒg‚ðŒ©‚Ä‚¢‚éScript")]
    [SerializeField] private LookAtTarget _lookAtTarget;


    // Update is called once per frame
    void Update()
    {
        if (_lookAtTarget.isInTheRange == false) { return; }

        count += Time.deltaTime;
        if(count > _createInterval)
        {
            Instantiate(_bulletPrefab,transform.position,transform.rotation);
            count = 0.0f;
        }
    }
}
