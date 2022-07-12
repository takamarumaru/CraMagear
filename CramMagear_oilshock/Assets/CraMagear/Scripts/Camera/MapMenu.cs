using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapMenu : MonoBehaviour
{
    //[SerializeField] Camera _MapMenuCam;
    [SerializeField] GameObject _MapMenuPanel;
    bool _MapMenuflags = false;
    float _Time = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ŒÄ‚Î‚ê‚Ä‚é");
        _MapMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(_MapMenuflags == false)
            {
                _MapMenuflags = true;
                _MapMenuPanel.SetActive(true);
               
            }
            else
            {
                _MapMenuflags = false;
                _MapMenuPanel.SetActive(false);
            }
        }
        
    }
}
