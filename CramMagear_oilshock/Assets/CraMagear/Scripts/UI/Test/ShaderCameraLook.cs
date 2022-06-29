using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCameraLook : MonoBehaviour
{

    [SerializeField] private Transform _self;

    [SerializeField] private Camera MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _self.LookAt(MainCamera.transform);
    }
}
