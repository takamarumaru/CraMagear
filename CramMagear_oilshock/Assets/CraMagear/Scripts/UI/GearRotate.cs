using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GearRotate : MonoBehaviour
{
    [SerializeField] Image _Gear1;
    [SerializeField] Image _Gear2;

    [SerializeField] float _GearRotateTime = 0.5f;

    private bool _pushFlag;
    private float _CountTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _pushFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        _CountTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _pushFlag = true;
            if(_pushFlag == true)
            {
                _CountTime = 0.0f;

                //‘Š‘Î“I‚É’l‚ð•Ï‰»‚³‚¹‚½‚¢‚Æ‚«‚ÍSetRelative(true)‚ðŽg‚¤
                _Gear1.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 90.0f), _GearRotateTime,RotateMode.Fast/*Beyond360*/).SetRelative(true).SetEase(Ease.Linear);
                _Gear2.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, -90.0f), _GearRotateTime,RotateMode.Fast/*Beyond360*/).SetRelative(true).SetEase(Ease.Linear);
                _pushFlag = false;
            }
            else
            {

                if (_CountTime > _GearRotateTime)
                {
                    _pushFlag = true;

                }
            }
            
        }
    }
}
