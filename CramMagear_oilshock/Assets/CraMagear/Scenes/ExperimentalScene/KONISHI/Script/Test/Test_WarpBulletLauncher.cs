using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_WarpBulletLauncher : MonoBehaviour
{
    [SerializeField] GameObject _prefab;

    bool flag = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_prefab).SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            flag = !flag;
        }

        if (flag) Instantiate(_prefab).SetActive(true);
    }
}
