using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembersAdministrator : MonoBehaviour
{
    [SerializeField] private int _createNum = 0;

    [SerializeField] private Transform _createMember;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<_createNum;i++)
        {
            Instantiate(_createMember.gameObject,transform);
        }
    }
}
