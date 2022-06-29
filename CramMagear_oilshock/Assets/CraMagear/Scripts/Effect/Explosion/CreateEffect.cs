using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffect : MonoBehaviour
{
    [SerializeField] GameObject _effectObj;

    private void Awake()
    {
        Instantiate(_effectObj,transform.position,transform.rotation);
    }

}
