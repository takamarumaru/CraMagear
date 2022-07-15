using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    //[SerializeField] Camera _camera;
    [SerializeField] Image _image;
    [SerializeField] Image _imageFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _image.transform.rotation = Camera.main.gameObject.transform.rotation;
        _imageFrame.transform.rotation = Camera.main.gameObject.transform.rotation;
    }
}
