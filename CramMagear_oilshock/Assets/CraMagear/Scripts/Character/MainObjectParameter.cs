using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjectParameter : MonoBehaviour
{
    [Tooltip("ƒ`[ƒ€ID")]
    [SerializeField] int _teamID = 0;
    public int TeamID { get=>_teamID; set => _teamID = value; }

    [Tooltip("–¼‘O")]
    [SerializeField] string _name;
    public string Name => _name;

    [Tooltip("‘Ì—Í")]
    [SerializeField] int _hp = 0;
    public int hp
    { 
        get => _hp;
        set 
        {
            _hp = value; 
            if (_hp < 0)_hp = 0;
        } 
    }

    private void Update()
    {
        if (_name != "Enemy")
        {
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
