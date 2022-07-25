using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModeChange : MonoBehaviour
{
    [SerializeField]
    ArchitectureCreator _architectureCreator;

    [SerializeField]
    BulletShot _bulletShot;

    public bool _enable { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BulletShot>().enabled = true;
        GetComponent<ArchitectureCreator>().enabled = false;
        //_enable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //�f�o�b�O�p�̉��̏����A�{����Player��State����Switching()���Ăяo��
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingLeft())
        {
            
        }
        
    }

    public void EnableToggle()
    {
        _enable = !_enable;
        
    }
}
