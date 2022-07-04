using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_WarpBulletLauncher : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _PlayerPos;

    bool flag = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_prefab,_PlayerPos.transform.position + _PlayerPos.transform.forward * 1.0f,Quaternion.identity).SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            flag = !flag;
        }

        if (flag) Instantiate(_prefab).SetActive(true);
    }
}
