using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChangeUI : MonoBehaviour
{
    [SerializeField] Image _GunIcon;
    [SerializeField] Image _ArchitectureIcon;
    [SerializeField] GameObject _SkillUI;

    private bool _ModeChange;
    // Start is called before the first frame update
    void Start()
    {
        _ModeChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            _ModeChange = true;
            if(_ModeChange == true)
            {
                _GunIcon.enabled = true;
                _SkillUI.gameObject.SetActive(true);

                _ArchitectureIcon.enabled = false;

                _ModeChange = false;
            }
            else
            {
                _GunIcon.enabled = false;
                _SkillUI.gameObject.SetActive(false);

                _ArchitectureIcon.enabled = true;

                _ModeChange = true;
            }
        }
    }
}
