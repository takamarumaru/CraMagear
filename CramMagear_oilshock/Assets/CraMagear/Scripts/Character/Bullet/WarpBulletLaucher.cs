using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBulletLaucher : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _PlayerPos;

    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WarpBulletShot();
        }
    }

    void WarpBulletShot()
    {
        Vector3 PlayerPos = _PlayerPos.transform.position;
        Quaternion PlayerRot = _PlayerPos.transform.rotation;
        Instantiate(_prefab, PlayerPos, PlayerRot).SetActive(true);
    }
}
